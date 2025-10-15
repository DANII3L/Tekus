using ApiRestTekus.Models;
/// <summary>
/// Interfaz de servicio de países
/// </summary>
public interface ICountry
{
    /// <summary>
    /// Obtiene la lista de países
    /// </summary>
    /// <param name="page">Página</param>
    /// <param name="search">Búsqueda por nombre</param>
    /// <returns>Lista de países</returns>
    Task<PagedResult<CountryModel>> GetCountries(int page, string? search = null);
}