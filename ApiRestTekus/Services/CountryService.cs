using System.Text.Json;
using ApiRestTekus.Models;

/// <summary>
/// Servicio de países
/// </summary>
public class CountryService : ICountry
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CountryService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Obtiene la lista de países
    /// </summary>
    /// <param name="page">Página</param>
    /// <param name="search">Búsqueda por nombre</param>
    /// <returns>Lista de países</returns>
    public async Task<PagedResult<CountryModel>> GetCountries(int page, string? search = null)
    {
        string url = string.IsNullOrWhiteSpace(search)
            ? "https://restcountries.com/v3.1/all?fields=cca2,name"
            : $"https://restcountries.com/v3.1/name/{search}?fields=cca2,name";

        var response = await _httpClientFactory.CreateClient().GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception("No se ha podido obtener la lista de países, error: " + response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var apiData = JsonSerializer.Deserialize<List<ApiCountry>>(json);

        if (apiData == null)
            throw new Exception("No se ha podido obtener la lista de países");

        var allCountries = apiData
            .Select(c => new CountryModel
            {
                Id = c.cca2 ?? "",
                Name = c.name?.common ?? ""
            })
            .OrderBy(c => c.Name);

        var countries = allCountries
            .Skip((page - 1) * 10)
            .Take(10)
            .ToList();

        return new PagedResult<CountryModel>
        {
            ListFind = countries,
            TotalRecords = allCountries.Count(),
            PageNumber = page,
            PageSize = 10
        };
    }

    private class ApiCountry
    {
        public string? cca2 { get; set; }
        public NameData? name { get; set; }
    }

    private class NameData
    {
        public string? common { get; set; }
    }
}