namespace ApiRestTekus.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<ICountry, CountryService>();
        services.AddScoped<IAuth, AuthService>();
        services.AddScoped<IProvider, ProviderService>();

        return services;
    }
}