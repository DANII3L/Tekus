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
    /// <returns>Lista de países</returns>
    public async Task<PagedResult<CountryModel>> GetCountries(int page)
    {
        var response = await _httpClientFactory.CreateClient().GetAsync("https://restcountries.com/v3.1/all?fields=cca2,name");
        
        if (!response.IsSuccessStatusCode)
            throw new Exception("No se ha podido obtener la lista de países, error: " + response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var apiData = JsonSerializer.Deserialize<List<ApiCountry>>(json);

        if (apiData == null)
            throw new Exception("No se ha podido obtener la lista de países");

        var countries = apiData
            .Select(c => new CountryModel
            {
                Id = c.cca2 ?? "",
                Name = c.name?.common ?? ""
            })
            .OrderBy(c => c.Name)
            .Skip((page - 1) * 10)
            .Take(10)
            .ToList();

        return new PagedResult<CountryModel>
        {
            ListFind = countries,
            TotalRecords = apiData.Count,
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