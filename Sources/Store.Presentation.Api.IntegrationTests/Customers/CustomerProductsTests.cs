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
        var response = await Api.Customer.Products.GetAvailableAsync();

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
            ReadProductTestModel.Create(TestProducts.Apples)
        };

        await Api.Admin
            .DeleteProductAsync(TestProducts.Bananas.Id)
            .EnsureIsSuccess();

        // Act
        var response = await Api.Customer.Products.GetAvailableAsync();

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContainContentAsync(expectedProducts);
    }

    [Fact]
    public async Task When_ProductDoesNotExist_Should_ReturnNotFound()
    {
        // Act
        var response = await Api.Customer.Products.FindAsync(TestProducts.UnknownId);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task When_ProductExists_Should_ReturnCorrectDetails()
    {
        // Arrange
        var expectedProduct = ReadProductTestModel.Create(TestProducts.Apples);

        // Act
        var response = await Api.Customer.Products.FindAsync(expectedProduct.Id);

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
        var response = await Api.Customer.Products.FindAsync(TestProducts.Apples.Id);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }
}