using ApiRestTekus.Models;

namespace ApiRestTekus.Interfaces
{
    public interface IProviderService
    {
        Task<PagedResult<ProviderServiceModel_Dto>> GetProviderServices(int page, string? search);
        Task<ProviderServiceModel_Dto> CreateProviderService(ProviderServiceModel providerService);
        Task<ProviderServiceModel_Dto> UpdateProviderService(ProviderServiceModel providerService);
        Task<IEnumerable<ProviderServiceModel_Dto>> GetProviderServicesByProviderId(int providerId);
        Task<ProviderServiceModel_Dto> DeleteProviderService(int id);
    }
}