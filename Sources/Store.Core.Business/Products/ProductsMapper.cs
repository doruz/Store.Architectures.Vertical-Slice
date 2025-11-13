using Store.Core.Domain.Entities;

namespace Store.Core.Business.Products;

internal static class ProductsMapper
{
    public static ProductModel ToProductModel(this Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = PriceModel.Create(product.Price),
        Stock = product.Stock
    };

    public static Product ToProduct(this NewProductModel model) => new
    (
        model.Name,
        model.Price,
        model.Stock
    );
}