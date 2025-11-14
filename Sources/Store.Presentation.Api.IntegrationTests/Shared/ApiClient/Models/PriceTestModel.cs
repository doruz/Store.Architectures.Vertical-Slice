namespace Store.Presentation.Api.IntegrationTests;

public record PriceTestModel(decimal Value)
{
    public string Currency => "€";
    public string Display => $"€{Value:F2}";
}