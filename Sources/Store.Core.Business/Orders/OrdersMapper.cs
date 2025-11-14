using Store.Core.Domain.Entities;
using Store.Core.Shared;

namespace Store.Core.Business.Orders;

internal static class OrdersMapper
{
    public static OrderSummaryModel ToOrderSummaryModel(this Order order) => new()
    {
        Id = order.Id,
        OrderedAt = order.CreatedAt.ToOrderedAt(),

        TotalProducts = order.TotalProducts,
        TotalPrice = PriceModel.Create(order.TotalPrice)
    };

    // TODO: to be moved
    public static ValueLabelModel<DateTime> ToOrderedAt(this DateTime orderedAt)
        => new(orderedAt, orderedAt.ToDateTimeString());
}