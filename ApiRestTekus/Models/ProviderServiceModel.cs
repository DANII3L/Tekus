using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ApiRestTekus.Models
{
    public class ProviderServiceModel
    {
        /// <summary>
        /// Obtener o establecer el id
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
        /// <summary>
        /// Obtener o establecer el id del proveedor
        /// </summary>
        [JsonPropertyName("providerId")]
        [Required]
        public int ProviderId { get; set; }
        /// <summary>
        /// Obtener o establecer el id del servicio
        /// </summary>
        [JsonPropertyName("serviceId")]
        [Required]
        public int ServiceId { get; set; }
        /// <summary>
        /// Obtener o establecer la lista de proveedores de servicio de países
        /// </summary>
        [JsonPropertyName("providerSeviceCountries")]
        [Required]
        public List<ProviderSeviceCountryModel> ProviderSeviceCountries { get; set; }
    }
}
