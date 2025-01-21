using Microsoft.AspNetCore.Http;
using MVCProject.Controllers.API.APIBase;

namespace MVCProject.Controllers.API.Base
{
    public class APIKeyValidation
    {
        //APIKeyValidation wordt niet gebruikt ; enkel APIKeyAttribute class wordt gebruikt
        //ppt les 8
        //https://code-maze.com/aspnetcore-api-key-authentication/
        //https://www.c-sharpcorner.com/article/using-api-key-authentication-to-secure-asp-net-core-web-api/
        //https://muratsuzen.medium.com/using-api-key-authorization-with-middleware-and-attribute-on-asp-net-core-web-api-543a4955a0ef

        private readonly RequestDelegate _requestDelegate;
        private const string ApiKey = "X-API-KEY";

        public APIKeyValidation(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
            Console.WriteLine("validation");
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("validation");
            if (!context.Request.Headers.TryGetValue(ApiKey, out var apiKeyVal))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key not found!");
            }

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();

            // aangepast voor meerdere keys
            IConfigurationSection apiKeysection = appSettings.GetSection(ApiKey + ":Keys");
            Boolean ValidKey = false;

            foreach (IConfigurationSection ApiKey in apiKeysection.GetChildren())
            {
                if (ApiKey.GetValue<string>("Key").Equals(apiKeyVal))
                {
                    ValidKey = true;

                    await _requestDelegate(context);
                }
            }

            if (!ValidKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client");
            }
        }
    }
}