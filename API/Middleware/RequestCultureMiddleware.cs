using System.Globalization;

namespace API.Middleware;

public class RequestCultureMiddleware
{
    private readonly RequestDelegate _next;

    public RequestCultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        CultureInfo culture;

        var cultureQuery = context.Request.Query["lang"];
        if (!string.IsNullOrWhiteSpace(cultureQuery))
        {
            culture = new CultureInfo(ConvertCultureExtensions.CultureCodeToStandardCultureCode(cultureQuery));
        }
        else
        {
            var cultureHeader = context.Request.Headers["Accept-Language"];
            if (!string.IsNullOrWhiteSpace(cultureHeader))
            {
                culture = new CultureInfo(ConvertCultureExtensions.CultureCodeToStandardCultureCode(cultureHeader));
            }
            else
            {
                culture = new CultureInfo(ConvertCultureExtensions.CultureCodeToStandardCultureCode("en"));
            }
        }

        Thread.CurrentThread.CurrentUICulture = culture;

        return _next(context);
    }
}
