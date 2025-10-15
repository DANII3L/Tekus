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
    /// <returns>Lista de países</returns>
    Task<PagedResult<CountryModel>> GetCountries(int page);
}