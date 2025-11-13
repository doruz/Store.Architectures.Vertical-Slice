using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.InMemory;

internal sealed class InMemoryProductsRepository(InMemoryDatabase database) : IProductsRepository
{
    public Task<IEnumerable<Product>> GetAllAsync() 
        => Task.FromResult(database.Products.AsEnumerable());

    public Task<IEnumerable<Product>> FilterAsync(Func<Product, bool> filter)
        => Task.FromResult(database.Products.Where(filter));

    public Task<bool> ExistsAsync(string id)
        => Task.FromResult(database.Products.Any(p => p.Id.IsEqualTo(id)));

    public Task<Product?> FindAsync(string id)
        => Task.FromResult(database.Products.Find(p => p.Id.IsEqualTo(id)));

    public Task AddAsync(Product product)
    {
        EnsureArg.IsNotNull(product, nameof(product));

        database.Products.Add(product);

        return Task.CompletedTask;
    }

    public async Task UpdateAsync(Product product)
    {
        EnsureArg.IsNotNull(product, nameof(product));

        await DeleteAsync(product.Id);
        await AddAsync(product);
    }

    public Task DeleteAsync(string id)
        => Task.FromResult(database.Products.RemoveAll(p => p.Id.IsEqualTo(id)));
}