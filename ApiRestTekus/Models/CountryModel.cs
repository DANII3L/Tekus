using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

/// <summary>
/// Modelo de país
/// </summary>
public class CountryModel
{
    /// <summary>
    /// Obtener o establecer el id
    /// </summary>
    [Required]
    [JsonPropertyName("id")]
    public string Id { get; set; }
    /// <summary>
    /// Obtener o establecer el nombre
    /// </summary>
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}