using EnsureThat;

namespace Store.Core.Domain.Entities;

public sealed record ShoppingCartLine
{
    public string ProductId { get; }
    public int Quantity { get; }

    public ShoppingCartLine(string productId, int quantity)
    {
        ProductId = CheckProductId(productId);
        Quantity = CheckQuantity(quantity);
    }

    public ShoppingCartLine WithQuantity(int quantity) => new(ProductId, quantity);
    public ShoppingCartLine IncreaseQuantity(int quantity) => new(ProductId, Quantity + quantity);

    private static string CheckProductId(string productId)
    {
        EnsureArg.IsNotNullOrEmpty(productId, nameof(productId));
        EnsureArg.IsNotEmptyOrWhiteSpace(productId, nameof(productId));

        return productId;
    }

    private static int CheckQuantity(int quantity)
    {
        return EnsureArg.IsGte(quantity, 0, nameof(quantity));
    }
}