using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controlador de países
/// </summary>
[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class CountryController : ControllerBase
{
    /// <summary>
    /// Servicio de países
    /// </summary>
    private readonly ICountry CountryService;

    /// <summary>
    /// Constructor del controlador de países
    /// </summary>
    /// <param name="country">Servicio de países</param>
    public CountryController(ICountry _CountryService)
    {
        CountryService = _CountryService;
    }
    /// <summary>
    /// Obtiene la lista de países
    /// </summary>
    /// <param name="page">Página</param>
    /// <param name="search">Búsqueda por nombre</param>
    /// <returns>Lista de países</returns>
    [HttpGet]
    public async Task<IActionResult> GetCountries(int? page, string? search)
    {
        var countries = await CountryService.GetCountries(page ?? 1, search);
        return Ok(new {
            data = countries,
            success = true,
            message = "Países obtenidos correctamente"
        });
    }
}