using System.Net.Http.Json;

namespace Store.Presentation.Api.IntegrationTests;

public sealed class CustomerApiClient(HttpClient client)
{
    private const string BaseRoute = "/api/customers/current";

    public CustomerShoppingCartApiClient Cart { get; } = new(client);

    public CustomerOrdersApiClient Orders { get; } = new(client);

    public Task<HttpResponseMessage> FindProductAsync(string id)
        => client.GetAsync($"{BaseRoute}/products/{id}");
}


public sealed class CustomerShoppingCartApiClient(HttpClient client)
{
    private const string Route = "/api/customers/current/shopping-carts/current";

    public Task<HttpResponseMessage> GetAsync()
        => client.GetAsync(Route);

    public Task<HttpResponseMessage> ClearAsync()
        => client.DeleteAsync(Route);

    public Task<HttpResponseMessage> UpdateAsync(Action<UpdateShoppingCartTestModel> shoppingCartActions)
    {
        var shoppingCart = new UpdateShoppingCartTestModel();

        shoppingCartActions(shoppingCart);

        return client.PatchAsJsonAsync(Route, shoppingCart.Lines);
    }

    public Task<HttpResponseMessage> CheckoutAsync()
        => client.PostAsJsonAsync($"{Route}/checkout", new { });
}


public sealed class CustomerOrdersApiClient(HttpClient client)
{
    private const string Route = "/api/customers/current/orders";

    public Task<HttpResponseMessage> GetAllAsync()
        => client.GetAsync(Route);

    public Task<HttpResponseMessage> FindAsync(string orderId)
        => client.GetAsync($"{Route}/{orderId}");
}