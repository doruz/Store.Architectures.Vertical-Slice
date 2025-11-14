namespace Store.Core.Business.Orders;

public sealed record FindCustomerOrderQueryResult
{
    public required string Id { get; init; }

    public required ValueLabelModel<DateTime> OrderedAt { get; init; }

    public required int TotalProducts { get; init; }
    public required PriceModel TotalPrice { get; init; }

    public required IReadOnlyList<OrderDetailedLineModel> Lines { get; init; } = [];
}

public sealed record OrderDetailedLineModel
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required PriceModel ProductPrice { get; init; }

    public required int Quantity { get; init; }

    public required PriceModel TotalPrice { get; init; }
}