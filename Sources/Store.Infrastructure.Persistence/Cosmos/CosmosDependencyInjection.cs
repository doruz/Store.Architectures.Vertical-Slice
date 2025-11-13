using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.Cosmos;

internal static class CosmosDependencyInjection
{
    internal static IServiceCollection AddCosmosPersistence(this IServiceCollection services, IConfiguration configuration) =>
        services
            .Configure<CosmosOptions>(configuration.GetRequiredSection(nameof(CosmosOptions)).Bind)

            .AddSingleton(CosmosClientFactory.Create(configuration))
            .AddSingleton<CosmosDatabaseContainers>()
            .AddSingleton<IAppInitializer, CosmosDatabaseInitializer>()

            .AddSingleton<IProductsRepository, CosmosProductsRepository>()
            .AddSingleton<IShoppingCartsRepository, CosmosShoppingCartsRepository>()
            .AddSingleton<IOrdersRepository, CosmosOrdersRepository>();
}