namespace Store.Core.Domain.Entities;

internal static class ShoppingCartLineExtensions
{
    internal static IEnumerable<ShoppingCartLine> Merge(this IEnumerable<ShoppingCartLine> lines) =>
        lines
            .GroupBy(line => line.ProductId)
            .Select(group => new ShoppingCartLine(group.Key, group.Sum(l => l.Quantity)));
}