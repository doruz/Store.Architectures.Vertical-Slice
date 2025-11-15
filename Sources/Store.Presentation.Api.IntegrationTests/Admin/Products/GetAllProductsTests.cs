namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

public class GetAllProductsTests(ApiApplicationFactory factory) : ApiBaseTests(factory)
{
    [Fact]
    public async Task When_ProductsAreRetrieved_Should_ReturnCorrectDetails()
    {
        // Arrange
        var expectedProducts = new List<ReadProductTestModel>
        {
            ReadProductTestModel.Create(TestProducts.Apples),
            ReadProductTestModel.Create(TestProducts.Bananas),
            ReadProductTestModel.Create(TestProducts.Oranges)
        };

        // Act
        var response = await Api.Admin.GetAllProductsAsync();

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContainContentAsync(expectedProducts);
    }

    [Fact]
    public async Task When_ProductIsDeleted_Should_NotBeReturned()
    {
        // Arrange
        var expectedProducts = new List<ReadProductTestModel>
        {
            ReadProductTestModel.Create(TestProducts.Apples),
            ReadProductTestModel.Create(TestProducts.Oranges)
        };

        await Api.Admin
            .DeleteProductAsync(TestProducts.Bananas.Id)
            .EnsureIsSuccess();

        // Act
        var response = await Api.Admin.GetAllProductsAsync();

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContainContentAsync(expectedProducts);
    }
}