using EnsureThat;

namespace Store.Core.Domain.Entities;

public sealed class Product(string name, Price price, int stock) : BaseEntity
{
    public string Name { get; private set; } = EnsureArg.IsNotNullOrEmpty(name);

    public Price Price { get; private set; } = EnsureArg.IsNotNull(price);

    public int Stock { get; private set; } = stock;

    public void Update(string? name, decimal? price, int? stock)
    {
        Name = EnsureArg.IsNotNullOrEmpty(name ?? Name);
        Price = EnsureArg.IsNotNull(price ?? Price);
        Stock = stock ?? Stock;
    }

    public bool StockIsAvailable(int quantity) => quantity.IsInRange(0, Stock);

    public void DecreaseStock(int quantity) => Stock -= quantity;
}