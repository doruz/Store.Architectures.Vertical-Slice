using Store.Core.Domain.Entities;

namespace Store.Core.Domain.Repositories;

public interface IShoppingCartsRepository
{
    Task<ShoppingCart?> FindAsync(string id);
    
    async Task<ShoppingCart> FindOrEmptyAsync(string id)
        => await FindAsync(id) ?? ShoppingCart.Empty(id);

    Task AddOrUpdateAsync(ShoppingCart cart);

    Task DeleteAsync(string id); 
}