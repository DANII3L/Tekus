namespace ApiRestTekus.Extensions;

public static class CorsExtensions
{
    /// <summary>
    /// Agrega la configuración de CORS
    /// </summary>
    /// <param name="services">Servicios</param>
    /// <returns>Servicios</returns>
    public static IServiceCollection AddCorsConfiguration(
        this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            /// <summary>
            /// Política para FrontendTekusAngular
            /// </summary>
            /// <param name="policy">Política</param>
            /// <returns>Política</returns>
            options.AddPolicy("AllowFrontendTekusAngular", policy =>
            {
                policy.WithOrigins(
                    "http://localhost:4200",
                    "https://localhost:4200"
                )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
        });

        return services;
    }
}