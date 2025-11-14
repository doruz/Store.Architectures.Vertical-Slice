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
}