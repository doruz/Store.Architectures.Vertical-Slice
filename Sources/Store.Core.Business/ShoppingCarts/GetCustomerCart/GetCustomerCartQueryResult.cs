namespace Store.Core.Business.ShoppingCarts;

public record GetCustomerCartQueryResult
{
    public IEnumerable<ShoppingCartLineModel> Lines { get; init; } = [];

    public required PriceModel TotalPrice { get; init; }
}

public record ShoppingCartLineModel
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required PriceModel ProductPrice { get; init; }

    public required int Quantity { get; init; }

    public required PriceModel TotalPrice { get; init; }
}
