using System.Net.Http.Json;

namespace Store.Presentation.Api.IntegrationTests;

public sealed class CustomerApiClient(HttpClient client)
{
    private const string BaseRoute = "/api/customers/current";

    public CustomerProductsApiClient Products { get; } = new(client, BaseRoute);

    public CustomerShoppingCartApiClient Cart { get; } = new(client, BaseRoute);

    public CustomerOrdersApiClient Orders { get; } = new(client, BaseRoute);
}


public sealed class CustomerProductsApiClient(HttpClient client, string baseRoute)
{
    private readonly string _route = $"{baseRoute}/products";

    public Task<HttpResponseMessage> GetAvailableAsync()
        => client.GetAsync(_route);

    public Task<HttpResponseMessage> FindAsync(string productId)
        => client.GetAsync($"{_route}/{productId}");
}


public sealed class CustomerShoppingCartApiClient(HttpClient client, string baseRoute)
{
    private readonly string _route = $"{baseRoute}/shopping-carts/current";

    public Task<HttpResponseMessage> GetAsync()
        => client.GetAsync(_route);

    public Task<HttpResponseMessage> ClearAsync()
        => client.DeleteAsync(_route);

    public Task<HttpResponseMessage> UpdateAsync(Action<UpdateShoppingCartTestModel> shoppingCartActions)
    {
        var shoppingCart = new UpdateShoppingCartTestModel();

        shoppingCartActions(shoppingCart);

        return client.PatchAsJsonAsync(_route, shoppingCart.Lines);
    }

    public Task<HttpResponseMessage> CheckoutAsync()
        => client.PostAsJsonAsync($"{_route}/checkout", new { });
}


public sealed class CustomerOrdersApiClient(HttpClient client, string baseRoute)
{
    private readonly string _route = $"{baseRoute}/orders";

    public Task<HttpResponseMessage> GetAllAsync()
        => client.GetAsync(_route);

    public Task<HttpResponseMessage> FindAsync(string orderId)
        => client.GetAsync($"{_route}/{orderId}");
}