using ApiRestTekus.Models;

namespace ApiRestTekus.Interfaces
{
    /// <summary>
    /// Interfaz de servicio
    /// </summary>
    public interface IService
    {
        Task<PagedResult<ServiceModel>> GetServices(int page, string? search);
        Task<ServiceModel> UpdateService(ServiceModel service);
        Task<ServiceModel> CreateService(ServiceModel service);
    }
}