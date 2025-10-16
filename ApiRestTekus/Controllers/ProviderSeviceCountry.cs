using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiRestTekus.Models;
using ApiRestTekus.Interfaces;

/// <summary>
/// Controlador de proveedores de servicio de países
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProviderSeviceCountryController : ControllerBase
{
    private readonly IProviderSeviceCountry _providerSeviceCountry;
    public ProviderSeviceCountryController(IProviderSeviceCountry providerSeviceCountry)
    {
        _providerSeviceCountry = providerSeviceCountry;
    }

    /// <summary>
    /// Obtiene la lista de proveedores
    /// </summary>
    /// <param name="page">Página</param>
    /// <param name="filter">Filtro</param>
    /// <returns>Lista de proveedores</returns>
    [HttpGet]
    public async Task<IActionResult> GetProviderSeviceCountries(int? page, string? search)
    {
        var providerSeviceCountries = await _providerSeviceCountry.GetProviderSeviceCountries(page ?? 1, search);
        return Ok(new {
            data = providerSeviceCountries,
            success = true,
            message = "Proveedores obtenidos correctamente"
        });
    }

    /// <summary>
    /// Crea un nuevo proveedor
    /// </summary>
    /// <param name="providerSeviceCountry">Proveedor a crear</param>
    /// <param name="providerServiceId">ID del proveedor de servicio</param>
    /// <returns>Proveedores de servicio de países creados correctamente</returns>
    [HttpPost]
    public async Task<IActionResult> CreateProviderSeviceCountry([FromBody] List<ProviderSeviceCountryModel> providerSeviceCountries, int providerServiceId)
    {
        var result = await _providerSeviceCountry.CreateProviderSeviceCountries(providerSeviceCountries, providerServiceId);
        return Ok(new {
            data = result,
            success = true,
            message = "Proveedores de servicio de países creados correctamente"
        });
    }
}