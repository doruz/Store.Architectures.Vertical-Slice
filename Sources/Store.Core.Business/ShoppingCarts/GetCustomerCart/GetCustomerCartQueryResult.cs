namespace Store.Core.Business.ShoppingCarts;

public sealed record GetCustomerCartQueryResult
{
    public IEnumerable<GetCustomerCartLineModel> Lines { get; init; } = [];

    public required PriceModel TotalPrice { get; init; }
}

public sealed record GetCustomerCartLineModel
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required PriceModel ProductPrice { get; init; }

    public required int Quantity { get; init; }

    public required PriceModel TotalPrice { get; init; }
}
