using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

/// <summary>
/// Modelo de autenticación
/// </summary>
public class AuthModel
{
    /// <summary>
    /// Obtener o establecer el nombre de usuario
    /// </summary>
    [Required]
    [JsonPropertyName("username")]
    public string Username { get; set; }
    /// <summary>
    /// Obtener o establecer la contraseña
    /// </summary>
    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}