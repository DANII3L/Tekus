using ApiRestTekus.Models;

namespace ApiRestTekus.Interfaces
{
    public interface IProvider
    {
        Task<PagedResult<ProviderModel>> GetProviders(int page, string? filter = null);
        Task<ProviderModel> CreateProvider(ProviderModel provider);
        Task<ProviderModel> UpdateProvider(ProviderModel provider);
    }
}