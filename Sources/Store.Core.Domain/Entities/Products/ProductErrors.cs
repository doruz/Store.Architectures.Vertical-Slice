namespace Store.Core.Domain.Entities;

public static class ProductErrors
{
    public static async Task<Product> EnsureIsNotNull(this Task<Product?> product, string productId)
        => await product ?? throw NotFound(productId);

    public static Product EnsureIsNotNull(this Product? product, string productId)
        => product ?? throw NotFound(productId);

    private static AppError NotFound(string productId)
        => AppError.NotFound("product_not_found", productId);

    public static Product EnsureStockIsAvailable(this Product product, int quantity) =>
        product.StockIsAvailable(quantity)
            ? product
            : throw AppError.Conflict("product_stock_not_available", product.Id);
}