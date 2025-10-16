using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuth _authService;
    public AuthController(IAuth authService)
    {
        _authService = authService;
    }
    /// <summary>
    /// Inicia sesión de usuario
    /// </summary>
    /// <param name="model">Credenciales del usuario</param>
    /// <returns>Token JWT</returns>
    /// <response code="200">Login exitoso</response>
    /// <response code="401">Credenciales inválidas</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] AuthModel model)
    {
        var token = await _authService.Login(model.Username, model.Password);
        return Ok(new {
            data = token,
            success = true,
            message = "Login exitoso"
        });
    }
}