using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class DomainRegistration
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        #region MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        #endregion

        return services;
    }
}
