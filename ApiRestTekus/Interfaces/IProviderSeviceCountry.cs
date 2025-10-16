using ApiRestTekus.Models;

namespace ApiRestTekus.Interfaces
{
    public interface IProviderSeviceCountry
    {
        Task<PagedResult<ProviderSeviceCountryModel>> GetProviderSeviceCountries(int page, string? search);
        Task<IEnumerable<ProviderSeviceCountryModel>> CreateProviderSeviceCountries(List<ProviderSeviceCountryModel> providerSeviceCountries, int providerServiceId);
        Task<IEnumerable<ProviderSeviceCountryModel>> GetProviderSeviceCountriesByProviderServiceIds(string filter);
    }
}