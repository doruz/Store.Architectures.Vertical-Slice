using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.InMemory;

internal sealed class InMemoryShoppingCartsRepository(InMemoryDatabase database) : IShoppingCartsRepository
{
    public Task<ShoppingCart?> FindAsync(string id)
        => Task.FromResult(database.ShoppingCarts.Find(cart => cart.Id.IsEqualTo(id)));

    public Task AddAsync(ShoppingCart cart)
    {
        database.ShoppingCarts.Add(EnsureArg.IsNotNull(cart, nameof(cart)));

        return Task.CompletedTask;
    }

    public async Task AddOrUpdateAsync(ShoppingCart cart)
    {
        await DeleteAsync(cart.Id);
        await AddAsync(cart);
    }

    public Task DeleteAsync(string id)
    {
        database.ShoppingCarts.RemoveAll(cart => cart.Id.IsEqualTo(id));

        return Task.CompletedTask;
    }
}