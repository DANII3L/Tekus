using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiRestTekus.Models
{
    /// <summary>
    /// Modelo de proveedores
    /// </summary>
    public class ProviderModel
    {
        /// <summary>
        /// Obtener o establecer el nombre
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        /// <summary>
        /// Obtener o establecer la descripción
        /// </summary>
        [Required]
        [JsonPropertyName("nit")]
        public string Nit { get; set; }
        /// <summary>
        /// Obtener o establecer la dirección
        /// </summary>
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// Obtener o establecer el email
        /// </summary>
        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}