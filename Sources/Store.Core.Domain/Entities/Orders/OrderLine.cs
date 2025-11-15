using EnsureThat;

namespace Store.Core.Domain.Entities;

public sealed record OrderLine
{
    public string ProductId { get; }

    public string ProductName { get; }

    public Price ProductPrice { get; }

    public int Quantity { get; }

    public Price TotalPrice => ProductPrice * Quantity;

    public OrderLine(string productId, string productName, Price productPrice, int quantity)
    {
        ProductId = EnsureArg.IsNotNullOrEmpty(productId);
        ProductName = EnsureArg.IsNotNullOrEmpty(productName);
        ProductPrice = EnsureArg.IsNotNull(productPrice);
        Quantity = EnsureArg.IsGte(quantity, 0);
    }

    public static OrderLine Create(ShoppingCartLine cartLine, Product product)
    {
        EnsureArg.IsTrue(cartLine.ProductId.IsEqualTo(product.Id));
        EnsureArg.IsInRange(cartLine.Quantity, 0, product.Stock);

        return new OrderLine
        (
            product.Id,
            product.Name,
            product.Price,

            cartLine.Quantity
        );
    }
}