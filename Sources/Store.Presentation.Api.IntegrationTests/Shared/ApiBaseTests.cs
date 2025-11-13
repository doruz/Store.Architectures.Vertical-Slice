using Store.Core.Shared;

namespace Store.Presentation.Api.IntegrationTests;

[Collection("ApiTestsCollection")]
public abstract class ApiBaseTests(ApiApplicationFactory factory) : IAsyncLifetime
{
    internal TestCosmosDatabase Database { get; } = factory.GetService<TestCosmosDatabase>();

    protected StoreApiClient Api { get; } = new(factory.CreateDefaultClient());

    protected ICurrentCustomer CurrentCustomer { get; } = factory.GetService<ICurrentCustomer>();

    public async Task InitializeAsync()
    {
        await Database.EnsureIsInitialized();
    }

    public Task DisposeAsync()
    {
        Api.Dispose();

        return Task.CompletedTask;
    }
}