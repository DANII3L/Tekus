/// <summary>
/// Servicio de autenticación
/// </summary>
public class AuthService : IAuth
{
    /// <summary>
    /// Inicia sesión de usuario
    /// </summary>
    /// <param name="username">Nombre de usuario</param>
    /// <param name="password">Contraseña</param>
    /// <returns>Token JWT</returns>
    public string Login(string username, string password)
    {
        return "Token JWT";
    }
}