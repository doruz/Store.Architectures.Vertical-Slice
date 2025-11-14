using Store.Core.Domain.Entities;

namespace Store.Core.Domain.Repositories;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetAsync(Func<Product, bool> filter);

    Task<bool> ExistsAsync(string id);

    Task<Product?> FindAsync(string id);

    Task AddAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(string id);
}