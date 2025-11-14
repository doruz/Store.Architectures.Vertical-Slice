namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

public class DeleteProductTests(ApiApplicationFactory factory) : ApiBaseTests(factory)
{
    [Fact]
    public async Task When_ProductDoesNotExist_Should_ReturnNotFound()
    {
        // Act
        var response = await Api.Admin.DeleteProductAsync(TestProducts.UnknownId);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task When_ProductExists_Should_ReturnNoContent()
    {
        // Act
        var response = await Api.Admin.DeleteProductAsync(TestProducts.Apples.Id);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task When_ProductWasDeleted_Should_ReturnNotFound()
    {
        // Act
        var responses = await Api.ExecuteTwice(api => api.Admin.DeleteProductAsync(TestProducts.Apples.Id));

        // Assert
        responses[0].Should().HaveStatusCode(HttpStatusCode.NoContent);
        responses[1].Should().HaveStatusCode(HttpStatusCode.NotFound);
    }
}