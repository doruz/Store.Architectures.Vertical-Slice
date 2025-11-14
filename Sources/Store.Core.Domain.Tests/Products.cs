using Store.Core.Domain.Entities;

namespace Store.Core.Domain.Tests;

internal static class Products
{
    public static readonly Product First = new("First", 0.5m, 10);

    public static readonly Product Second = new("Second", 0.99m, 10);
}