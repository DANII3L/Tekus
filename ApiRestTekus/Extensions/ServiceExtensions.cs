using ApiRestTekus.Interfaces;
namespace ApiRestTekus.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IData, DataService>();
        services.AddScoped<ICountry, CountryService>();
        services.AddScoped<IAuth, AuthService>();
        services.AddScoped<IProvider, ProviderService>();
        services.AddScoped<IService, ServiceService>();
        services.AddScoped<IProviderSeviceCountry, ProviderSeviceCountryService>();
        services.AddScoped<IProviderService, ProviderServiceService>();
        
        return services;
    }
}