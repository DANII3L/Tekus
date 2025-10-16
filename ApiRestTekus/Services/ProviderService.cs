using ApiRestTekus.Models;
using ApiRestTekus.Interfaces;
/// <summary>
/// Servicio de proveedores
/// </summary>
public class ProviderService : IProvider
{
    private readonly IData _dataService;
    private readonly string[] columns = new string[] { "Id", "Name", "Nit", "Email" };
    private readonly string table = "Provider";
    public ProviderService(IData dataService)
    {
        _dataService = dataService;
    }

    /// <summary>
    /// Obtiene la lista de proveedores
    /// </summary>
    /// <param name="page">Página</param>
    /// <param name="filter">Filtro global para buscar en todas las columnas</param>
    /// <returns>Lista de proveedores</returns>
    public async Task<PagedResult<ProviderModel>> GetProviders(int page, string? filter = null)
    {
        // Validar que la página sea válida
        int validPage = Math.Max(1, page);
        
        string sqlFilter = SqlFilterBuilder.BuildLikeFilter(columns, filter ?? "", table);

        var result = await _dataService.EjecutarProcedimientoAsync<ProviderModel>(
            "crud_select_provider",
            new { PageNumber = validPage, Filter = sqlFilter, PageSize = 10 }
        );

        if (!result.IsSuccess)
            throw new Exception("No se ha podido obtener la lista de proveedores, error: " + result.Message);

        return PagedResult<ProviderModel>.PaginatedResponse(result, validPage);
    }

    public async Task<ProviderModel> CreateProvider(ProviderModel provider)
    {
        var result = await _dataService.EjecutarProcedimientoAsync<ProviderModel>(
            "crud_insert_provider",
            new { Nit = provider.Nit, Name = provider.Name, Email = provider.Email }
        );

        if (!result.IsSuccess)
            throw new Exception("No se ha podido crear el proveedor, error: " + result.Message);

        return result.Data.FirstOrDefault() ?? provider;
    }

    public async Task<ProviderModel> UpdateProvider(ProviderModel provider)
    {
        var result = await _dataService.EjecutarProcedimientoAsync<ProviderModel>(
            "crud_update_provider",
            new { Id = provider.Id, Nit = provider.Nit, Name = provider.Name, Email = provider.Email }
        );

        if (!result.IsSuccess)
            throw new Exception("No se ha podido actualizar el proveedor, error: " + result.Message);

        return result.Data.FirstOrDefault() ?? provider;
    }
}