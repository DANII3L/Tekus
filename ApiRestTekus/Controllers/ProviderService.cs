using ApiRestTekus.Interfaces;
using ApiRestTekus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controlador de proveedores de servicio
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProviderServiceController : ControllerBase
{
    private readonly IProviderService _providerService;
    public ProviderServiceController(IProviderService ProviderService)
    {
        _providerService = ProviderService; 
    }

    /// <summary>
    /// Obtiene la lista de proveedores
    /// </summary>
    /// <param name="page">Página</param>
    /// <param name="filter">Filtro</param>
    /// <returns>Lista de proveedores</returns>
    [HttpGet]
    public async Task<IActionResult> GetProviderServices(int? page, string? search)
    {
        var providerServices = await _providerService.GetProviderServices(page ?? 1, search);
        return Ok(new {
            data = providerServices,
            success = true,
            message = "Proveedores obtenidos correctamente"
        });
    }

    /// <summary>
    /// crear un nuevo proveedor
    /// </summary>
    /// <param name="providerService">Proveedor a crear</param>
    /// <returns>Proveedor creado</returns>
    [HttpPost]
    public async Task<IActionResult> CreateProviderService([FromBody] ProviderServiceModel providerService)
    {
        var result = await _providerService.CreateProviderService(providerService);
        return Ok(new {
            data = result,
            success = true,
            message = "Proveedor creado correctamente"
        });
    }

    /// <summary>
    /// Actualiza un proveedor
    /// </summary>
    /// <param name="id">ID del proveedor</param>
    /// <param name="providerService">Proveedor a actualizar</param>
    /// <returns>Proveedor actualizado</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateProviderService([FromBody] ProviderServiceModel providerService)
    {
        var result = await _providerService.UpdateProviderService(providerService);
        return Ok(new {
            data = result,
            success = true,
            message = "Proveedor actualizado correctamente"
        });
    }

    /// <summary>
    /// Obtiene los proveedores de servicio por id de proveedor
    /// </summary>
    /// <param name="providerId">ID del proveedor</param>
    /// <returns>Lista de proveedores de servicio</returns>
    [HttpGet("provider/{providerId}")]
    public async Task<IActionResult> GetProviderServicesByProviderId([FromRoute] int providerId)
    {
        var result = await _providerService.GetProviderServicesByProviderId(providerId);
        return Ok(new {
            data = result,
            success = true,
            message = "Proveedores de servicio obtenidos correctamente"
        });
    }

    /// <summary>
    /// Elimina un proveedor
    /// </summary>
    /// <param name="id">ID del proveedor</param>
    /// <returns>Proveedor eliminado</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProviderService([FromRoute] int id)
    {
        var result = await _providerService.DeleteProviderService(id);
        return Ok(new {
            data = result,
            success = true,
            message = "Proveedor eliminado correctamente"
        });
    }
}