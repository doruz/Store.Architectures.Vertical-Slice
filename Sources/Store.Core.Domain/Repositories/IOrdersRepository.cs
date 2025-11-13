using Store.Core.Domain.Entities;

namespace Store.Core.Domain.Repositories;

public interface IOrdersRepository
{
    Task<IEnumerable<Order>> GetCustomerOrdersAsync(string customerId);

    Task<Order?> FindOrderAsync(string customerId, string id);

    Task SaveOrderAsync(Order order);
}