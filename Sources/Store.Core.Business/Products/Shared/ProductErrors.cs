using Store.Core.Domain.Entities;

namespace Store.Core.Business.Products;

internal static class ProductErrors
{
    public static BusinessException NotFound(string productId) 
        => new (BusinessError.NotFound("product_not_found", productId));

    public static async Task<Product> EnsureIsNotNull(this Task<Product?> product, string productId)
        => await product ?? throw NotFound(productId);

    public static Product EnsureIsNotNull(this Product? product, string productId) 
        => product ?? throw NotFound(productId);

    public static Product EnsureStockIsAvailable(this Product product, int quantity) =>
        product.StockIsAvailable(quantity)
            ? product
            : throw new BusinessException(BusinessError.Conflict("product_stock_not_available", product.Id));
}