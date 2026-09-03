using Microsoft.Extensions.DependencyInjection;
using ProductsMicroService.Core.UseCases.Category.Register;
using ProductsMicroService.Core.UseCases.Product.Register;

namespace ProductsMicroService.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IRegisterProductUseCase, RegisterProductUseCase>();
        services.AddScoped<IRegisterCategoryUseCase, RegisterCategoryUseCase>();
        return services;
    }
}