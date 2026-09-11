using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using ProductsMicroService.Core.UseCases.Category.Register;
using ProductsMicroService.Core.UseCases.Product.Delete;
using ProductsMicroService.Core.UseCases.Product.Get;
using ProductsMicroService.Core.UseCases.Product.List;
using ProductsMicroService.Core.UseCases.Product.Register;

namespace ProductsMicroService.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddUseCases();
        services.AddFluentValidationConfiguration();
        services.AddMapsterConfiguration();

        return services;
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterProductUseCase, RegisterProductUseCase>();
        services.AddScoped<IRegisterCategoryUseCase, RegisterCategoryUseCase>();
        services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
        services.AddScoped<IListProductsUseCase, ListProductsUseCase>();
        services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
    }

    private static void AddMapsterConfiguration(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }

    private static void AddFluentValidationConfiguration(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterProductValidator>();
    }
}