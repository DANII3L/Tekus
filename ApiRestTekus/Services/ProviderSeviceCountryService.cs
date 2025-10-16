using ApiRestTekus.Interfaces;
using ApiRestTekus.Models;
/// <summary>
/// Servicio de proveedores de servicio de países
/// </summary>
public class ProviderSeviceCountryService : IProviderSeviceCountry
{
    private readonly IData _dataService;
    private readonly string[] columns = new string[] { "Id", "ProviderServiceId", "CountryId" };
    private readonly string table = "psc";
    public ProviderSeviceCountryService(IData dataService)
    {
        _dataService = dataService;
    }

    public async Task<PagedResult<ProviderSeviceCountryModel>> GetProviderSeviceCountries(int page, string? search)
    {
        int validPage = Math.Max(1, page);
        string sqlFilter = SqlFilterBuilder.BuildLikeFilter(columns, search ?? "", table);
        var result = await _dataService.EjecutarProcedimientoAsync<ProviderSeviceCountryModel>(
            "crud_select_provider_service_country",
            new { PageNumber = validPage, Filter = sqlFilter, PageSize = 10 }
        );

        if (!result.IsSuccess)
            throw new Exception("No se ha podido obtener la lista de proveedores de servicio de países, error: " + result.Message);

        return PagedResult<ProviderSeviceCountryModel>.PaginatedResponse(result, validPage);
    }

    public async Task<IEnumerable<ProviderSeviceCountryModel>> GetProviderSeviceCountriesByProviderServiceIds(string filter)
    {
        var result = await _dataService.EjecutarProcedimientoAsync<ProviderSeviceCountryModel>(
            "crud_select_provider_service_country",
            new { Filter = filter, PageSize = int.MaxValue }
        );

        if (!result.IsSuccess)
            throw new Exception("No se ha podido obtener la lista de proveedores de servicio de países, error: " + result.Message);

        return result.Data.ToList();
    }

    public async Task<IEnumerable<ProviderSeviceCountryModel>> CreateProviderSeviceCountries(List<ProviderSeviceCountryModel> providerSeviceCountries, int providerServiceId)
    {
        await _dataService.EjecutarProcedimientoAsync<ProviderSeviceCountryModel>(
            "crud_delete_provider_service_country",
            new { ProviderServiceId = providerServiceId }
            );

        var results = new List<ProviderSeviceCountryModel>();
        foreach (var providerSeviceCountry in providerSeviceCountries)
        {
            var result = await _dataService.EjecutarProcedimientoAsync<ProviderSeviceCountryModel>(
            "crud_insert_provider_service_country",
            new { ProviderServiceId = providerServiceId, CountryId = providerSeviceCountry.CountryId }
            );

            if (!result.IsSuccess)
                throw new Exception("No se ha podido actualizar el proveedor de servicio de países, error: " + result.Message);

            results.Add(result.Data.FirstOrDefault() ?? providerSeviceCountry);
        }

        return results;
    }
}