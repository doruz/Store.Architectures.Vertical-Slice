using Microsoft.Extensions.DependencyInjection;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.InMemory;

internal static class InMemoryDependencyInjection
{
    internal static IServiceCollection AddInMemoryPersistence(this IServiceCollection services) =>
        services
            .AddSingleton<InMemoryDatabase>()
            .AddTransient<IAppInitializer, InMemoryCollectionsInitializer>()

            .AddSingleton<IProductsRepository, InMemoryProductsRepository>()
            .AddSingleton<IShoppingCartsRepository, InMemoryShoppingCartsRepository>()
            .AddSingleton<IOrdersRepository, InMemoryOrdersRepository>();
}