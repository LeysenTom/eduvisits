using Microsoft.AspNetCore.Mvc.Filters;

namespace MVCProject.Controllers.API.APIBase
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class APIkeyAttribute : Attribute, IAsyncActionFilter
    {
        //zie APIKeyValidation voor bronnen
        private const string ApiKey = "X-API-KEY";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKey, out var apiKeyVal))
            {
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("Api Key not found!");
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            ;

            IConfigurationSection apiKeysection = appSettings.GetSection(ApiKey + ":Keys");
            Boolean ValidKey = false;

            foreach (IConfigurationSection ApiKey in apiKeysection.GetChildren())
            {
                if (ApiKey.GetValue<string>("Key").Equals(apiKeyVal))
                {
                    ValidKey = true;
                    await next.Invoke();
                }
            }

            if (!ValidKey)
            {
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("Unauthorized client");
            }
        }
    }
}