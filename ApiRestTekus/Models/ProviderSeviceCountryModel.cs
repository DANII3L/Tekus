using System.Text.Json.Serialization;

namespace ApiRestTekus.Models
{
    public class ProviderSeviceCountryModel
    {
        /// <summary>
        /// Obtener o establecer el id
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
        /// <summary>
        /// Obtener o establecer el id del proveedor
        /// </summary>
        [JsonPropertyName("providerServiceId")]
        public int ProviderServiceId { get; set; }
        /// <summary>
        /// Obtener o establecer el id del país
        /// </summary>
        [JsonPropertyName("countryId")]
        public string CountryId { get; set; }
    }
}
