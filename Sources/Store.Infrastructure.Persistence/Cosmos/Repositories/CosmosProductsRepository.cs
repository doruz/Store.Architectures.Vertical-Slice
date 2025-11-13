using EnsureThat;
using Microsoft.Azure.Cosmos;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Microsoft.Azure.Cosmos.Linq;

namespace Store.Infrastructure.Persistence.Cosmos;

internal sealed class CosmosProductsRepository(CosmosDatabaseContainers containers) : IProductsRepository
{
    public Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = containers.Products
            .GetItemLinqQueryable<Product>(true)
            .AsEnumerable();

        return Task.FromResult(products);
    }

    public Task<IEnumerable<Product>> FilterAsync(Func<Product, bool> filter)
    {
        var products = containers.Products
            .GetItemLinqQueryable<Product>(true)
            .Where(filter)
            .AsEnumerable();

        return Task.FromResult(products);
    }

    public async Task<bool> ExistsAsync(string id)
    {
        EnsureArg.IsNotNullOrEmpty(id, nameof(id));

        var requestOptions = new QueryRequestOptions
        {
            PartitionKey = id.ToPartitionKey(),
            MaxItemCount = 1
        };

        return await containers.Products
            .GetItemLinqQueryable<Product>(requestOptions: requestOptions)
            .Where(product => product.Id == id)
            .CountAsync() > 0;
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

    public Task DeleteAsync(string id) 
        => containers.Products.DeleteItemAsync<Product>(id, id.ToPartitionKey());
}