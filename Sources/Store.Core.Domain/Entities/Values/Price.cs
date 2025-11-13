using EnsureThat;

namespace Store.Core.Domain.Entities;

public sealed record Price
{
    public decimal Value { get; }
    public string Currency => "€";

    public Price(decimal value)
    {
        Value = EnsureArg.IsGte(value, 0, nameof(value));
    }

    public override string ToString() => $"{Currency}{Value:F2}";

    public static implicit operator Price(decimal priceValue) => new(priceValue);
    public static Price operator +(Price price1, Price price2) => new(price1.Value + price2.Value);
    public static Price operator *(Price price, int quantity) => new(price.Value * quantity);
}