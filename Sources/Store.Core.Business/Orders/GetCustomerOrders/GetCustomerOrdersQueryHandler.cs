using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.Orders;

internal sealed class GetCustomerOrdersQueryHandler(RepositoriesContext repositories, ICurrentCustomer currentCustomer)
    : IRequestHandler<GetCustomerOrdersQuery, IEnumerable<OrderSummaryModel>>
{
    public async Task<IEnumerable<OrderSummaryModel>> Handle(GetCustomerOrdersQuery request, CancellationToken _)
    {
        var orders = await repositories.Orders.GetCustomerOrdersAsync(currentCustomer.Id);

        return orders
            .OrderByDescending(order => order.CreatedAt)
            .Select(ToOrderSummaryModel);
    }

    private static OrderSummaryModel ToOrderSummaryModel(Order order) => new()
    {
        Id = order.Id,
        OrderedAt = order.CreatedAt.ToOrderedAt(),

        TotalProducts = order.TotalProducts,
        TotalPrice = PriceModel.Create(order.TotalPrice)
    };
}