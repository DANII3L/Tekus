using ApiRestTekus.Interfaces;
using ApiRestTekus.Models;
public class ProviderServiceService : IProviderService  {
    private readonly IData _dataService;
    private readonly IProviderSeviceCountry _providerSeviceCountry;
    private readonly string[] columns = new string[] { "ps.Id", "ps.ProviderId", "ps.ServiceId", "s.Name" };
    public ProviderServiceService(IData dataService, IProviderSeviceCountry providerSeviceCountry)
    {
        _dataService = dataService;
        _providerSeviceCountry = providerSeviceCountry;
    }

    public async Task<PagedResult<ProviderServiceModel_Dto>> GetProviderServices(int page, string? search)
    {
        int validPage = Math.Max(1, page);
        string sqlFilter = SqlFilterBuilder.BuildLikeFilter(columns, search ?? "");
        var result = await _dataService.EjecutarProcedimientoAsync<ProviderServiceModel_Dto>(
            "crud_select_provider_service",
            new { PageNumber = validPage, Filter = sqlFilter, PageSize = 10 }
        );
        if (!result.IsSuccess)
            throw new Exception("No se ha podido obtener la lista de proveedores de servicio, error: " + result.Message);

        return PagedResult<ProviderServiceModel_Dto>.PaginatedResponse(result, validPage);
    }

    public async Task<IEnumerable<ProviderServiceModel_Dto>> GetProviderServicesByProviderId(int providerId)
    {
        var result = await _dataService.EjecutarProcedimientoAsync<ProviderServiceModel_Dto>(
            "crud_select_provider_service",
            new { ProviderId = providerId, PageSize = int.MaxValue }
        );
        if (!result.IsSuccess)
            throw new Exception("No se ha podido obtener la lista de proveedores de servicio, error: " + result.Message);

        var listIds = result.Data.ToList().Select(p => p.Id.ToString());
        var idsInProviders = ToSqlIn(listIds, id => $"'{id}'");
        var filter = "";
        if (idsInProviders != "" )
            filter = $" AND psc.ProviderServiceId IN ({idsInProviders})";

        var resultCountries = await _providerSeviceCountry.GetProviderSeviceCountriesByProviderServiceIds(filter);

        result.Data.ToList().ForEach(p => {
            if (p.ProviderSeviceCountries == null)
                p.ProviderSeviceCountries = new List<ProviderSeviceCountryModel>();
            p.ProviderSeviceCountries.AddRange(resultCountries.Where(c => c.ProviderServiceId == p.Id).ToList());
        });

        return result.Data.ToList();
    }

    private string ToSqlIn(IEnumerable<string> list, Func<string, string> func)
    {
        return string.Join(",", list.Select(func).Distinct());
    }

    public async Task<ProviderServiceModel_Dto> CreateProviderService(ProviderServiceModel providerService)
    {
        var result = await _dataService.EjecutarProcedimientoAsync<ProviderServiceModel_Dto>(
            "crud_insert_provider_service",
            new { ProviderId = providerService.ProviderId, ServiceId = providerService.ServiceId }
        );

        if (!result.IsSuccess)
            throw new Exception("No se ha podido crear el proveedor de servicio, error: " + result.Message);

        providerService.Id = result.Data.FirstOrDefault()?.Id ?? 0;
        var resultCountries = await _providerSeviceCountry.CreateProviderSeviceCountries(providerService.ProviderSeviceCountries, providerService.Id);
        if (!resultCountries.Any())
            throw new Exception("No se ha podido crear los países del proveedor de servicio");

        return result.Data.FirstOrDefault() ?? new ProviderServiceModel_Dto();
    }

    public async Task<ProviderServiceModel_Dto> UpdateProviderService(ProviderServiceModel providerService)
    {
        var result = await _dataService.EjecutarProcedimientoAsync<ProviderServiceModel_Dto>(
            "crud_update_provider_service",
            new { Id = providerService.Id, ProviderId = providerService.ProviderId, ServiceId = providerService.ServiceId }
        );

        if (!result.IsSuccess)
            throw new Exception("No se ha podido actualizar el proveedor de servicio, error: " + result.Message);

        var resultCountries = await _providerSeviceCountry.CreateProviderSeviceCountries(providerService.ProviderSeviceCountries, providerService.Id);
        if (!resultCountries.Any())
            throw new Exception("No se ha podido actualizar los países del proveedor de servicio");

        return result.Data.FirstOrDefault() ?? new ProviderServiceModel_Dto();
    }

    public async Task<ProviderServiceModel_Dto> DeleteProviderService(int id)
    {
        var result = await _dataService.EjecutarProcedimientoAsync<ProviderServiceModel_Dto>(
            "crud_delete_provider_service",
            new { Id = id }
        );
        if (!result.IsSuccess)
            throw new Exception("No se ha podido eliminar el proveedor de servicio, error: " + result.Message);

        return result.Data.FirstOrDefault() ?? new ProviderServiceModel_Dto();
    }
}