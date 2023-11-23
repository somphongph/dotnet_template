using Domain.Enumerables;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Domain.Filters
{
    public class AppAuthenFilter : IAsyncActionFilter
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public AppAuthenFilter(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }


        async Task IAsyncActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string? requestAppId = context.HttpContext.Request.Headers["X-App-Id"];
            string? requestAppKey = context.HttpContext.Request.Headers["X-App-Key"];
            string? settingAppId;
            string? settingAppKey;

            //Checking from REQUEST
            if (string.IsNullOrEmpty(requestAppId) || string.IsNullOrEmpty(requestAppKey))
            {
                Unauthorized(context);
                return;
            }

            // Get from CACHE
            if (_memoryCache.TryGetValue(requestAppId!, out string? result))
            {
                settingAppId = requestAppId!;
                settingAppKey = result ?? String.Empty;
            }
            else
            {
                // Get from Auth-API
                (settingAppId, settingAppKey) = await GetKeyVault(requestAppId!);
                if (string.IsNullOrEmpty(settingAppId) || string.IsNullOrEmpty(settingAppKey))
                {
                    Unauthorized(context);
                    return;
                }

                // Cache to InMemomryDatabase
                var expire = Convert.ToInt32(_configuration.GetValue<string>("CacheApiKey"));
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(expire));
                _memoryCache.Set(settingAppId!, settingAppKey, cacheEntryOptions);
            }

            // Key Validation
            if (requestAppId != settingAppId || requestAppKey != settingAppKey)
            {
                Unauthorized(context);
                return;
            }

            await next();
        }

        private void Unauthorized(ActionExecutingContext context)
        {
            var response = new ResponseCommand<object>(ResponseStatus.Unauthorized);
            var result = new JsonResult(response) { StatusCode = StatusCodes.Status401Unauthorized };
            context.Result = result;
        }

        private Task<(string settingAppId, string settingAppKey)> GetKeyVault(string v)
        {
            throw new NotImplementedException();
        }
    }
}
