namespace PMA_DocumentationService.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpClient _httpClient;

        public AuthorizationMiddleware(RequestDelegate next, IHttpClientFactory clientFactory)
        {
            _next = next;
            _httpClient = clientFactory.CreateClient("authClient");
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (context.Request.Headers.TryGetValue("Authorization", out var Token))
            {
                var StatusCode = await ProcessAuthRequest(Token);

                if (StatusCode == 200)
                {
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = StatusCode;

                    await context.Response.StartAsync();
                }
            }
            else
            {
                context.Response.StatusCode = 401;

                await context.Response.StartAsync();
            }

        }

        private async Task<int> ProcessAuthRequest(string Token)
        {
            var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    _httpClient.BaseAddress + "api/v1/identity/validation");

            request.Headers.Add("Authorization", Convert.ToString(Token));

            var response = await _httpClient.SendAsync(request);

            return (int)response.StatusCode;
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
