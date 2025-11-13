namespace Store.Presentation.Api.IntegrationTests;

public record ValidationError(string Property, params string[] Messages)
{
    internal KeyValuePair<string, string[]> ToKeyValuePair() => new(Property, Messages);
}