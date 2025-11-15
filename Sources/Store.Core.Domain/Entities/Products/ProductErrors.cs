namespace Store.Core.Domain.Entities;

public static class ProductErrors
{
    public static async Task<Product> EnsureExists(this Task<Product?> product, string productId)
        => EnsureExists(await product, productId);

    public static Product EnsureExists(this Product? product, string productId)
    {
        if(product is null || product.IsDeleted())
        {
            throw NotFound(productId);
        }

        return product;
    }

    private static AppError NotFound(string productId)
        => AppError.NotFound("product_not_found", productId);

    public static Product EnsureStockIsAvailable(this Product product, int quantity) =>
        product.StockIsAvailable(quantity)
            ? product
            : throw AppError.Conflict("product_stock_not_available", product.Id);
}