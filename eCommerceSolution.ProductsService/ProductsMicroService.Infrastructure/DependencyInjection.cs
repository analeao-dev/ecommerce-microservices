using Microsoft.Extensions.DependencyInjection;
using ProductsMicroService.Infrastructure.DbContext;

namespace ProductsMicroService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<DapperDbContext>();
        return services;
    }
}