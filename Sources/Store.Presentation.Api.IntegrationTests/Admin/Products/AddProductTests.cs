namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

public class AddProductTests(ApiApplicationFactory factory) : ApiBaseTests(factory)
{
    [Theory]
    [ClassData(typeof(AddProductValidationData))]
    public async Task When_ProductDetailsAreInvalid_Should_ReturnValidationErrors(NewProductTestModel invalidProduct, ValidationError expectedError)
    {
        // Act
        var response = await Api.Admin.AddProductAsync(invalidProduct);

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.BadRequest)
            .And.ContainValidationErrorAsync(expectedError);
    }

    [Fact]
    public async Task When_ProductDetailsAreValid_Should_ReturnAddedProductId()
    {
        // Arrange
        var newProduct = NewProductTestModel.CreateRandom();

        // Act
        var response = await Api.Admin.AddProductAsync(newProduct);

        // Assert
        await response.Should().ContainIdAsync();
    }
}
