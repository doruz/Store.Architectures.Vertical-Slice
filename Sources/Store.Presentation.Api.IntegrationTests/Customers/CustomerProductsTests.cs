namespace Store.Presentation.Api.IntegrationTests.Customers;

public class CustomerProductsTests(ApiApplicationFactory factory) : ApiBaseTests(factory)
{
    [Fact]
    public async Task When_AvailableProductsAreRetrieved_Should_ReturnCorrectDetails()
    {
        // Arrange
        var expectedProducts = new List<ReadProductTestModel>
        {
            ReadProductTestModel.Create(TestProducts.Apples),
            ReadProductTestModel.Create(TestProducts.Bananas)
        };

        // Act
        var response = await Api.Customer.Products.GetAvailableProductsAsync();

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContainContentAsync(expectedProducts);
    }

    [Fact]
    public async Task When_ProductDoesNotExist_Should_ReturnNotFound()
    {
        // Act
        var response = await Api.Customer.Products.FindProductAsync(TestProducts.UnknownId);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task When_ProductExists_Should_ReturnCorrectDetails()
    {
        // Arrange
        var expectedProduct = ReadProductTestModel.Create(TestProducts.Apples);

        // Act
        var response = await Api.Customer.Products.FindProductAsync(expectedProduct.Id);

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContainContentAsync(expectedProduct);
    }
}