using Store.Core.Domain.Entities;

namespace Store.Core.Business.Products;

public record ProductModel
{
    public required string Id { get; init; }

    public required string Name { get; init; } 

    public required PriceModel Price { get; init; }

    public required int Stock { get; init; }

    public static ProductModel Create(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = PriceModel.Create(product.Price),
        Stock = product.Stock
    };
}