namespace PMA_SagaService.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (context.Request.Headers.TryGetValue("Authorization", out var Token))
            {
                context.Response.Headers.Authorization = Token;

                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401;

                await context.Response.StartAsync();
            }

        }

    }

    public static class AuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthorizationMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
