namespace Store.Presentation.Api.IntegrationTests;

public record OrderDetailsTestModel : OrderSummaryTestModel
{
    public required IReadOnlyList<OrderDetailsLineTestModel> Lines { get; init; } = [];
}


public record OrderDetailsLineTestModel
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required PriceTestModel ProductPrice { get; init; }

    public required int Quantity { get; init; }

    public required PriceTestModel TotalPrice { get; init; }
}