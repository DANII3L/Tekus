using System.Text.Json.Serialization;

namespace ApiRestTekus.Models
{
    public class ProviderServiceModel_Dto : ProviderServiceModel
    {
        /// <summary>
        /// Obtener o establecer el nombre del servicio
        /// </summary>
        [JsonPropertyName("serviceName")]
        public string ServiceName { get; set; }
        /// <summary>
        /// Obtener o establecer la tasa horaria
        /// </summary>
        [JsonPropertyName("hourlyRate")]
        public decimal HourlyRate { get; set; }
    }
}