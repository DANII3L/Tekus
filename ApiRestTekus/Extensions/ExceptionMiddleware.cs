using System.Net;
using System.Text.Json;

namespace ApiRestTekus.Middlewares
{
    /// <summary>
    /// Middleware global para el manejo de excepciones en la API.
    /// Captura cualquier excepción no manejada y devuelve una respuesta JSON estándar.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor del middleware de excepciones.
        /// </summary>
        /// <param name="next">El siguiente middleware en la cadena.</param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Método principal que intercepta la petición HTTP y maneja excepciones globalmente.
        /// </summary>
        /// <param name="context">Contexto HTTP.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Maneja la excepción y devuelve una respuesta JSON con el código de estado adecuado.
        /// </summary>
        /// <param name="context">Contexto HTTP.</param>
        /// <param name="exception">Excepción capturada.</param>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            if (exception is KeyNotFoundException)
                code = HttpStatusCode.NotFound;
            else if (exception is ArgumentException || exception is ArgumentNullException)
                code = HttpStatusCode.BadRequest;

            var result = JsonSerializer.Serialize(new
            {
                success = false,
                message = exception.Message,
                data = (object?)null
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}