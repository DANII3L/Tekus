using ApiRestTekus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiRestTekus.Interfaces;

/// <summary>
/// Controlador de proveedores
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProviderController : ControllerBase
{
    private readonly IProvider _providerService;
    public ProviderController(IProvider providerService)
    {
        _providerService = providerService;
    }

    /// <summary>
    /// Obtiene la lista de proveedores
    /// </summary>
    /// <param name="page">Página</param>
    /// <param name="filter">Filtro</param>
    /// <returns>Lista de proveedores</returns>
    [HttpGet]
    public async Task<IActionResult> GetProviders(int? page, string? search)
    {
        var providers = await _providerService.GetProviders(page ?? 1, search);
        return Ok(new {
            data = providers,
            success = true,
            message = "Proveedores obtenidos correctamente"
        });
    }

    /// <summary>
    /// Crea un nuevo proveedor
    /// </summary>
    /// <param name="provider">Proveedor a crear</param>
    /// <returns>Proveedor creado</returns>
    [HttpPost]
    public async Task<IActionResult> CreateProvider([FromBody] ProviderModel provider)
    {
        var result = await _providerService.CreateProvider(provider);
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
    /// <param name="provider">Proveedor a actualizar</param>
    /// <returns>Proveedor actualizado</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProvider(string id, [FromBody] ProviderModel provider)
    {
        var result = await _providerService.UpdateProvider(provider);
        return Ok(new {
            data = result,
            success = true,
            message = "Proveedor actualizado correctamente"
        });
    }
}