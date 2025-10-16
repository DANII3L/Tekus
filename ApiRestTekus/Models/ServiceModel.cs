using System.Text.Json.Serialization;

namespace ApiRestTekus.Models
{
    /// <summary>
    /// Modelo de servicio
    /// </summary>
    public class ServiceModel
    {
        /// <summary>
        /// Obtener o establecer el id
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        /// <summary>
        /// Obtener o establecer el nombre
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// Obtener o establecer la tasa horaria
        /// </summary>
        [JsonPropertyName("hourlyRate")]
        public decimal HourlyRate { get; set; }
    }
}