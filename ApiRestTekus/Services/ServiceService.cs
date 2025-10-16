using ApiRestTekus.Interfaces;
using ApiRestTekus.Models;
public class ServiceService : IService
{
    private readonly IData _dataService;
    private readonly string[] columns = new string[] { "Id", "Name" };
    private readonly string table = "Service";
    public ServiceService(IData dataService)
    {
        _dataService = dataService;
    }

    /// <summary>
    /// Obtiene la lista de servicios
    /// </summary>
    /// <param name="page">Página</param>
    /// <param name="search">Filtro global para buscar en todas las columnas</param>
    /// <returns>Lista de servicios</returns>
    public async Task<PagedResult<ServiceModel>> GetServices(int page, string? search = null)
    {
        // Validar que la página sea válida
        int validPage = Math.Max(1, page);
        
        string sqlFilter = SqlFilterBuilder.BuildLikeFilter(columns, search ?? "", table);
        var result = await _dataService.EjecutarProcedimientoAsync<ServiceModel>(
            "crud_select_service",
            new { PageNumber = validPage, Filter = sqlFilter, PageSize = 10 }
        );
        
        if (!result.IsSuccess)
            throw new Exception("No se ha podido obtener la lista de servicios, error: " + result.Message);

        return PagedResult<ServiceModel>.PaginatedResponse(result, validPage);
    }

    public async Task<ServiceModel> UpdateService(ServiceModel service)
    {

        if (service.Id == 0)
            throw new Exception("El id del servicio es requerido");

        var resultFind = await _dataService.EjecutarProcedimientoAsync<ServiceModel>(
            "crud_select_service",
            new { Id = service.Id }
        );

        if (!resultFind.IsSuccess)
            throw new Exception("No se ha podido obtener el servicio, error: " + resultFind.Message);
        
        if (resultFind.Data.FirstOrDefault() == null)
            throw new Exception("El servicio no existe");

        var result = await _dataService.EjecutarProcedimientoAsync<ServiceModel>(
            "crud_update_service",
            new { 
                Id = service.Id, 
                Name = service.Name, 
                HourlyRate = service.HourlyRate 
            }
        );

        if (!result.IsSuccess)
            throw new Exception("No se ha podido actualizar el servicio, error: " + result.Message);

        return result.Data.FirstOrDefault() ?? service;
    }

    public async Task<ServiceModel> CreateService(ServiceModel service)
    {
        var result = await _dataService.EjecutarProcedimientoAsync<ServiceModel>(
            "crud_insert_service",
            new { Name = service.Name, HourlyRate = service.HourlyRate }
        );

        if (!result.IsSuccess)
            throw new Exception("No se ha podido crear el servicio, error: " + result.Message);

        return result.Data.FirstOrDefault() ?? service;
    }
}