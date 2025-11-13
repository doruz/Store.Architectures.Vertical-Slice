namespace Store.Presentation.Api.IntegrationTests;

public record DateTimeTestModel(DateTime Value)
{
    public string Label => Value.ToString("dd MMM yyyy, HH:mm");
}