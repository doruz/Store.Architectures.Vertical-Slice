using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.Cosmos;

internal sealed class CosmosProductsRepository(CosmosDatabaseContainers containers) : IProductsRepository
{
    public Task<IEnumerable<Product>> GetAsync(Func<Product, bool> filter)
    {
        var products = containers.Products
            .GetItemLinqQueryable<Product>(true)
            .Where(filter)
            .AsEnumerable();

        return Task.FromResult(products);
    }

    public Task<Product?> FindAsync(string id)
        => containers.Products.FindAsync<Product>(id, id.ToPartitionKey());

    public async Task AddAsync(Product product)
    {
        EnsureArg.IsNotNull(product, nameof(product));

        await containers.Products.CreateItemAsync(product, product.Id.ToPartitionKey());
    }


    public async Task UpdateAsync(Product product)
    {
        EnsureArg.IsNotNull(product, nameof(product));

        await containers.Products.ReplaceItemAsync(product, product.Id, product.Id.ToPartitionKey());
    }
}