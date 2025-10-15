using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
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
    public IActionResult Login([FromBody] AuthModel model)
    {
        // Tu lógica aquí
        return Ok(new { token = "..." });
    }
}