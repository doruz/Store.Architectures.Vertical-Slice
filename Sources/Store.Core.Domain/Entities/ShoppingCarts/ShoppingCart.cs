using EnsureThat;

namespace Store.Core.Domain.Entities;

public sealed class ShoppingCart(params IEnumerable<ShoppingCartLine> lines) : BaseEntity
{
    public IReadOnlyList<ShoppingCartLine> Lines { get; private set; } = lines.ToList();

    public static ShoppingCart Empty(string customerId)
        => new() { Id = EnsureArg.IsNotNullOrEmpty(customerId) };

    public bool IsEmpty()
        => Lines.All(line => line.Quantity == 0);

    public void UpdateOrRemoveLines(params ShoppingCartLine[] lines)
        => lines.Merge().ForEach(UpdateOrRemoveLine);

    public void UpdateOrRemoveLine(ShoppingCartLine line)
    {
        EnsureArg.IsNotNull(line, nameof(line));

        RemoveLine(line.ProductId);

        if (line.Quantity > 0)
        {
            AddLine(line);
        }
    }

    private void RemoveLine(string productId)
    {
        EnsureArg.IsNotNullOrEmpty(productId, nameof(productId));

        Lines = Lines
            .Where(line => line.ProductId.IsNotEqualTo(productId))
            .ToList();
    }

    private void AddLine(ShoppingCartLine line)
    {
        EnsureArg.IsNotNull(line, nameof(line));

        Lines = [.. Lines, line];
    }
}