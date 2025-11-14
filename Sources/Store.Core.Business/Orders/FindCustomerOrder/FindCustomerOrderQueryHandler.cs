using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.Orders;

internal sealed class FindCustomerOrderQueryHandler(RepositoriesContext repositories, ICurrentCustomer currentCustomer)
    : IRequestHandler<FindCustomerOrderQuery, FindCustomerOrderQueryResult>
{
    public async Task<FindCustomerOrderQueryResult> Handle(FindCustomerOrderQuery request, CancellationToken _)
    {
        var order = await repositories.Orders
            .FindOrderAsync(currentCustomer.Id, request.OrderId)
            .EnsureIsNotNull(request.OrderId);

        return order.Map(ToOrderDetailedModel);
    }

    private static FindCustomerOrderQueryResult ToOrderDetailedModel(Order order) => new()
    {
        Id = order.Id,
        OrderedAt = order.CreatedAt.ToOrderedAt(),

        TotalProducts = order.TotalProducts,
        TotalPrice = PriceModel.Create(order.TotalPrice),

        Lines = order.Lines.Select(ToOrderLineModel).ToList()
    };

    private static OrderDetailedLineModel ToOrderLineModel(OrderLine product) => new()
    {
        ProductId = product.ProductId,
        ProductName = product.ProductName,
        ProductPrice = PriceModel.Create(product.ProductPrice),
        Quantity = product.Quantity,
        TotalPrice = PriceModel.Create(product.TotalPrice)
    };
}