using System.Text.Json.Serialization;

public class UserModel
{
    /// <summary>
    /// Obtener o establecer el nombre de usuario
    /// </summary>
    [JsonPropertyName("username")]
    public string Username { get; set; }
    /// <summary>
    /// Obtener o establecer la contraseña
    /// </summary>
    [JsonPropertyName("password")]
    public string Password { get; set; }
    /// <summary>
    /// Obtener o establecer el nombre completo
    /// </summary>
    [JsonPropertyName("fullName")]
    public string FullName { get; set; }
    /// <summary>
    /// Obtener o establecer el email
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; }
}