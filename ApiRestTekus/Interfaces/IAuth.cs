/// <summary>
/// Interfaz de autenticación
/// </summary>
public interface IAuth
{
    /// <summary>
    /// Inicia sesión de usuario
    /// </summary>
    /// <param name="username">Nombre de usuario</param>
    /// <param name="password">Contraseña</param>
    /// <returns>Token JWT</returns>
    Task<string> Login(string username, string password);
}