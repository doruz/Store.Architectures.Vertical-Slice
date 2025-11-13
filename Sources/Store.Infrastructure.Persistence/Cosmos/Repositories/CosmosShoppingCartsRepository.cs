using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.Cosmos;

internal sealed class CosmosShoppingCartsRepository(CosmosDatabaseContainers containers) : IShoppingCartsRepository
{
    public Task<ShoppingCart?> FindAsync(string id)
        => containers.ShoppingCarts.FindAsync<ShoppingCart>(id, id.ToPartitionKey());

    public async Task AddOrUpdateAsync(ShoppingCart cart)
    {
        EnsureArg.IsNotNull(cart, nameof(cart));

        await containers.ShoppingCarts.UpsertItemAsync(cart);
    }

    public Task DeleteAsync(string id)
        => containers.ShoppingCarts.DeleteAsync<ShoppingCart>(id, id.ToPartitionKey());
}