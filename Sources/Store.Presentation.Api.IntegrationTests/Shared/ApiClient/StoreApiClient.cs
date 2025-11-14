namespace Store.Presentation.Api.IntegrationTests;

public sealed class StoreApiClient(HttpClient httpClient) : IDisposable
{
    public AdminApiClient Admin { get; } = new(httpClient);

    public CustomerApiClient Customer { get; } = new(httpClient);

    public Task<List<HttpResponseMessage>> ExecuteTwice(Func<StoreApiClient, Task<HttpResponseMessage>> action)
        => ExecuteMultiple(action, 2);

    public async Task<List<HttpResponseMessage>> ExecuteMultiple(
        Func<StoreApiClient, Task<HttpResponseMessage>> action,
        int times)
    {
        var responses = new List<HttpResponseMessage>();

        for (int i = 0; i < times; i++)
        {
            responses.Add(await action(this));
        }

        return responses;
    }

    public void Dispose() => httpClient.Dispose();
}