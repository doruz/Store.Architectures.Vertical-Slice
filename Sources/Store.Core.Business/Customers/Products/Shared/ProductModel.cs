namespace Store.Core.Business.Customers.Products;

public record ProductModel
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required PriceModel Price { get; init; }

    public required int Stock { get; init; }
}