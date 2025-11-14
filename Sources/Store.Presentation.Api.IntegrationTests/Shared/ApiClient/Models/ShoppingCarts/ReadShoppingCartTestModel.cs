namespace Store.Presentation.Api.IntegrationTests;

public record ReadShoppingCartTestModel
{
    public List<ReadShoppingCartLineTestModel> Lines { get; init; } = [];
    public required PriceTestModel TotalPrice { get; init; }
}


public record ReadShoppingCartLineTestModel
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required PriceTestModel ProductPrice { get; set; }

    public required int Quantity { get; init; }

    public required PriceTestModel TotalPrice { get; init; }
}