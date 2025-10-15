using ApiRestTekus.Models;
public interface IProvider
{
    Task<PagedResult<ProviderModel>> GetProviders(int page);
}