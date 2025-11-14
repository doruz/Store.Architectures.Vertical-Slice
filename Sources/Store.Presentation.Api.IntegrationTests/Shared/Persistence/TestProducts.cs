using Store.Core.Domain.Entities;

namespace Store.Presentation.Api.IntegrationTests;

internal static class TestProducts
{
    public const string UnknownId = "Unknown";

    public static readonly Product Bananas = new("Bananas", 0.75m, 10)
    {
        Id = "b4f256a5-f65f-4811-a0d4-10d1fbba5f25"
    };

    public static readonly Product Apples = new("Apples", 0.99m, 5)
    {
        Id = "9b5055cf-6cd0-4086-8d01-6e1582a7fb0a"
    };

    public static readonly Product Oranges = new("Oranges", 0.55m, 0)
    {
        Id = "5c6a5841-1e91-4ff1-96d7-091affe74dc5"
    };

    public static IReadOnlyList<Product> All =
    [
        Apples,
        Bananas,
        Oranges
    ];
}