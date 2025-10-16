using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Servicio de autenticación
/// </summary>
public class AuthService : IAuth
{
    private readonly IConfiguration _configuration;
    private readonly IData _dataService;

    // Argon2 constants
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 4;
    private const int MemorySize = 65536;
    private const int DegreeOfParallelism = 2;

    public AuthService(IData dataService, IConfiguration configuration)
    {
        _dataService = dataService;
        _configuration = configuration;
    }

    public async Task<string> Login(string username, string password)
    {
        var result = await _dataService.EjecutarProcedimientoAsync<UserModel>(
                "crud_select_login",
                new { Username = username }
            );

        if (!result.IsSuccess)
            throw new UnauthorizedAccessException($"Login failed: {result.Message}");

        var user = result.Data.FirstOrDefault();

        if (user == null || !VerifyPassword(password, user.Password))
            throw new UnauthorizedAccessException("Invalid username or password");

        var token = GenerateJwtToken(user);

        return token;
    }

    #region Password Verification
    private bool VerifyPassword(string password, string hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
            return false;

        try
        {
            var combinedBytes = Convert.FromBase64String(hashedPassword);
            if (combinedBytes.Length != SaltSize + HashSize)
                return false;

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(combinedBytes, 0, salt, 0, SaltSize);

            var storedHashBytes = new byte[HashSize];
            Buffer.BlockCopy(combinedBytes, SaltSize, storedHashBytes, 0, HashSize);

            var newHash = CreateArgon2Hash(password, salt);

            return CryptographicOperations.FixedTimeEquals(storedHashBytes, newHash);
        }
        catch (FormatException)
        {
            return false;
        }
    }

    private byte[] CreateArgon2Hash(string password, byte[] salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = DegreeOfParallelism,
            Iterations = Iterations,
            MemorySize = MemorySize
        };

        return argon2.GetBytes(HashSize);
    }
    #endregion

    #region JWT Token Generation
    private string GenerateJwtToken(UserModel user)
    {
        var jwtKey = _configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT Key is not configured");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName)
        };
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    #endregion
}