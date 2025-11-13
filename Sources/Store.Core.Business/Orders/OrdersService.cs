using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.Orders;

public sealed class OrdersService(RepositoriesContext repositories, ICurrentCustomer currentCustomer)
{
    public async Task<IEnumerable<OrderSummaryModel>> GetCurrentCustomerOrders()
    {
        var orders = await repositories.Orders.GetCustomerOrdersAsync(currentCustomer.Id);

        return orders
            .OrderByDescending(order => order.CreatedAt)
            .Select(OrdersMapper.ToOrderSummaryModel);
    }

    public async Task<OrderDetailedModel> FindCurrentCustomerOrder(string id)
    {
        var order = await repositories.Orders
            .FindOrderAsync(currentCustomer.Id, id)
            .EnsureIsNotNull(id);

        return order.Map(OrdersMapper.ToOrderDetailedModel);
    }
}