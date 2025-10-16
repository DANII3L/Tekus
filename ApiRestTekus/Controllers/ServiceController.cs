using ApiRestTekus.Interfaces;
using ApiRestTekus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controlador de servicios
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ServiceController : ControllerBase
    {
    private readonly IService _serviceService;
    public ServiceController(IService serviceService)
    {
        _serviceService = serviceService;
    }

    /// <summary>
    /// Obtiene la lista de servicios
    /// </summary>
    /// <param name="page">Página</param>
    /// <param name="search">Búsqueda por nombre</param>
    /// <returns>Lista de servicios</returns>
    [HttpGet]
    public async Task<IActionResult> GetServices(int? page, string? search)
    {
        var services = await _serviceService.GetServices(page ?? 1, search);
        return Ok(new {
            data = services,
            success = true,
            message = "Services obtained successfully"
        });
    }

    /// <summary>
    /// Actualiza un servicio
    /// </summary>
    /// <param name="id">ID del servicio</param>
    /// <param name="service">Servicio a actualizar</param>
    /// <returns>Servicio actualizado</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateService([FromBody] ServiceModel service)
    {
        var result = await _serviceService.UpdateService(service);
        return Ok(new {
            data = result,
            success = true,
            message = "Service updated successfully"
        });
    }

    /// <summary>
    /// Crea un nuevo servicio
    /// </summary>
    /// <param name="service">Servicio a crear</param>
    /// <returns>Servicio creado</returns>
    [HttpPost]
    public async Task<IActionResult> CreateService([FromBody] ServiceModel service)
    {
        var result = await _serviceService.CreateService(service);
        return Ok(new {
            data = result,
            success = true,
            message = "Servicio creado correctamente"
        });
    }
}