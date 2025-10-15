using ApiRestTekus.Extensions;
using ApiRestTekus.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Tekus API",
        Version = "v1",
        Description = "API Rest Tekus",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Daniel Martin",
            Email = "bedomartin2@gmail.com"
        }
    });
});

builder.Services.AddApplicationServices();
builder.Services.AddCorsConfiguration();
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tekus API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowFrontendTekusAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


