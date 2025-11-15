namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

public class FindProductTests(ApiApplicationFactory factory) : ApiBaseTests(factory)
{
    [Fact]
    public async Task When_ProductDoesNotExist_Should_ReturnNotFound()
    {
        // Act
        var response = await Api.Admin.FindProductAsync(TestProducts.UnknownId);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task When_ProductExists_Should_ReturnCorrectDetails()
    {
        // Arrange
        var expectedProduct = ReadProductTestModel.Create(TestProducts.Apples);

        // Act
        var response = await Api.Admin.FindProductAsync(expectedProduct.Id);

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContainContentAsync(expectedProduct);
    }

    [Fact]
    public async Task When_ProductIsDeleted_Should_ReturnNotFound()
    {
        // Arrange
        await Api.Admin
            .DeleteProductAsync(TestProducts.Apples.Id)
            .EnsureIsSuccess();

        // Act
        var response = await Api.Admin.FindProductAsync(TestProducts.Apples.Id);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }
}