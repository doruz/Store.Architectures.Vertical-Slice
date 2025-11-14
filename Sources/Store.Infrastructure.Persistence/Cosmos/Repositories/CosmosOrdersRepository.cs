using EnsureThat;
using Microsoft.Azure.Cosmos;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.Cosmos;

internal sealed class CosmosOrdersRepository(CosmosDatabaseContainers containers) : IOrdersRepository
{
    public Task<IEnumerable<Order>> GetCustomerOrdersAsync(string customerId)
    {
        EnsureArg.IsNotNullOrEmpty(customerId, nameof(customerId));

        var requestOptions = new QueryRequestOptions
        {
            PartitionKey = customerId.ToPartitionKey()
        };

        var orders = containers.Orders
            .GetItemLinqQueryable<Order>(true, requestOptions: requestOptions)
            .AsEnumerable();

        return Task.FromResult(orders);
    }

    public Task<Order?> FindOrderAsync(string customerId, string id) 
        => containers.Orders.FindAsync<Order>(id, customerId.ToPartitionKey());

    public async Task SaveOrderAsync(Order order)
    {
        EnsureArg.IsNotNull(order, nameof(order));

        await containers.Orders.UpsertItemAsync(order);
    }
}