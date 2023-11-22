using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region MongoDB
        services.Configure<MongoSettings>(
            configuration.GetSection(nameof(MongoSettings)));

        services.AddSingleton<IMongoSettings>(sp =>
            sp.GetRequiredService<IOptions<MongoSettings>>().Value);
        #endregion

        #region Repositories dependency injection
        services.AddSingleton<IMongoContext, MongoContext>();
        #endregion

        return services;
    }
}
