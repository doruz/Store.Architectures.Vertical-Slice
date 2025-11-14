using System.Net.Http.Json;

namespace Store.Presentation.Api.IntegrationTests;

public sealed class AdminApiClient(HttpClient client)
{
    private const string ProductsRoute = "/api/admins/products";

    public Task<HttpResponseMessage> GetAllProductsAsync()
        => client.GetAsync(ProductsRoute);

    public Task<HttpResponseMessage> FindProductAsync(string productId)
        => client.GetAsync($"{ProductsRoute}/{productId}");

    public Task<HttpResponseMessage> AddProductAsync(NewProductTestModel product)
        => client.PostAsJsonAsync(ProductsRoute, product);

    public Task<HttpResponseMessage> EditProductAsync(string productId, object product)
        => client.PutAsJsonAsync($"{ProductsRoute}/{productId}", product);

    public Task<HttpResponseMessage> DeleteProductAsync(string productId)
        => client.DeleteAsync($"{ProductsRoute}/{productId}");
}