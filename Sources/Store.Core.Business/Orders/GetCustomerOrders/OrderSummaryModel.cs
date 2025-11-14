namespace Store.Core.Business.Orders;

public record OrderSummaryModel
{
    public required string Id { get; init; }

    public required ValueLabelModel<DateTime> OrderedAt { get; init; }

    public required int TotalProducts { get; init; }
    public required PriceModel TotalPrice { get; init; }
}