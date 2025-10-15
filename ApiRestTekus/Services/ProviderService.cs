using ApiRestTekus.Models;
/// <summary>
/// Servicio de proveedores
/// </summary>
public class ProviderService : IProvider
{
    /// <summary>
    /// Obtiene la lista de proveedores
    /// </summary>
    /// <param name="page">Página</param>
    /// <returns>Lista de proveedores</returns>
    public Task<PagedResult<ProviderModel>> GetProviders(int page)
    {
        return Task.FromResult(new PagedResult<ProviderModel>());
    }
}