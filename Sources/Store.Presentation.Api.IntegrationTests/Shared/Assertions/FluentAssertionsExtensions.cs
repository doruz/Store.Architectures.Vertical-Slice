namespace Store.Presentation.Api.IntegrationTests;

internal static class FluentAssertionsExtensions
{
    public static HttpResponseAssertions Should(this HttpResponseMessage response) => new(response);
}