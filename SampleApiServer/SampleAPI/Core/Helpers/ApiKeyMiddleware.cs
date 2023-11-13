namespace SampleAPI.Core.ServiceLayer.Helpers
{
    /// <summary>
    /// Using API Key Authentication To Secure ASP.NET Core Web API
    /// https://www.c-sharpcorner.com/article/using-api-key-authentication-to-secure-asp-net-core-web-api/
    /// </summary>
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEY = "XApiKey";
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // StatusCode: 401
            if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
                throw new AppException("Api Key was not provided.");
            
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(APIKEY);
            if (!apiKey.Equals(extractedApiKey))
                throw new AppException("Unauthorized client.");

            await _next(context);
        }
    }
}