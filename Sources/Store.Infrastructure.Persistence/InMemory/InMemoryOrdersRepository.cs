using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.InMemory;

internal sealed class InMemoryOrdersRepository(InMemoryDatabase database) : IOrdersRepository
{
    public Task<IEnumerable<Order>> GetCustomerOrdersAsync(string customerId)
    {
        EnsureArg.IsNotNullOrEmpty(customerId, nameof(customerId));

        var customerOrders = database.Orders
            .Where(order => order.CustomerId.IsEqualTo(customerId));

        return Task.FromResult(customerOrders);
    }

    public Task<Order?> FindOrderAsync(string customerId, string id)
    {
        EnsureArg.IsNotNullOrEmpty(customerId, nameof(customerId));
        EnsureArg.IsNotNullOrEmpty(id, nameof(id));

        var customerOrder = database.Orders
            .FirstOrDefault(order => order.CustomerId.IsEqualTo(customerId) && order.Id.IsEqualTo(id));

        return Task.FromResult(customerOrder);
    }

    public Task SaveOrderAsync(Order order)
    {
        EnsureArg.IsNotNull(order, nameof(order));

        database.Orders.Add(order);

        return Task.CompletedTask;
    }
}