namespace Store.Presentation.Api.IntegrationTests;

public record OrderSummaryTestModel
{
    public required string Id { get; init; }

    public required DateTimeTestModel OrderedAt { get; init; }

    public required int TotalProducts { get; init; }

    public required PriceTestModel TotalPrice { get; init; }
}