namespace Store.Core.Domain.Entities;

public static class PriceExtensions
{
    public static Price Sum(this IEnumerable<Price> prices)
    {
        return prices.IsNotEmpty()
            ? prices.Aggregate((price1, price2) => price1 + price2)
            : 0;
    }
}