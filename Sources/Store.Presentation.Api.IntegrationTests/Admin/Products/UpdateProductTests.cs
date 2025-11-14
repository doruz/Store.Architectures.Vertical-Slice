namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

public class UpdateProductTests(ApiApplicationFactory factory) : ApiBaseTests(factory)
{
    [Theory]
    [ClassData(typeof(UpdateProductValidationData))]
    public async Task When_ProductDetailsAreInvalid_Should_ReturnValidationErrors(object invalidDetails, ValidationError expectedError)
    {
        // Act
        var response = await Api.Admin.EditProductAsync(TestProducts.UnknownId, invalidDetails);

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.BadRequest)
            .And.ContainValidationErrorAsync(expectedError);
    }

    [Fact]
    public async Task When_ProductDoesNotExist_Should_ReturnNotFound()
    {
        // Act
        var response = await Api.Admin.EditProductAsync(TestProducts.UnknownId, new {});

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task When_ProductExists_Should_ReturnNoContent()
    {
        // Act
        var response = await Api.Admin.EditProductAsync(TestProducts.Apples.Id, new {});

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task When_NoUpdatesAreProvided_Should_KeepSameDetails()
    {
        // Arrange
        var expectedProduct = ReadProductTestModel.Create(TestProducts.Apples);

        // Act
        await UpdateProductDetails(expectedProduct.Id, new { });

        // Assert
        await CheckProductDetails(expectedProduct);
    }

    [Fact]
    public async Task When_NewNameIsProvided_Should_UpdateWithTheNewValue()
    {
        // Arrange
        var expectedProduct = ReadProductTestModel.Create(TestProducts.Apples) with
        {
            Name = "Red Apples"
        };

        // Act
        await UpdateProductDetails(expectedProduct.Id, new { Name = "Red Apples" });

        // Assert
        await CheckProductDetails(expectedProduct);
    }

    [Fact]
    public async Task When_NewPriceIsProvided_Should_UpdateWithTheNewValue()
    {
        // Arrange
        var expectedProduct = ReadProductTestModel.Create(TestProducts.Apples) with
        {
            Price = new(1.75m)
        };

        // Act
        await UpdateProductDetails(expectedProduct.Id, new { Price = 1.75 });

        // Assert
        await CheckProductDetails(expectedProduct);
    }

    [Fact]
    public async Task When_NewStockIsProvided_Should_UpdateWithTheNewValue()
    {
        // Arrange
        var expectedProduct = ReadProductTestModel.Create(TestProducts.Apples) with
        {
            Stock = 10
        };

        // Act
        await UpdateProductDetails(expectedProduct.Id, new { Stock = 10 });

        // Assert
        await CheckProductDetails(expectedProduct);
    }

    [Fact]
    public async Task When_AllDetailsProvided_Should_UpdateWithTheNewValues()
    {
        // Arrange
        var expectedProduct = ReadProductTestModel.Create(TestProducts.Apples) with
        {
            Name = "Green Apples",
            Stock = 25,
            Price = new (0.19m)
        };

        // Act
        await UpdateProductDetails(expectedProduct.Id, new
        {
            Name = "Green Apples",
            Stock = 25,
            Price = 0.19
        });

        // Assert
        await CheckProductDetails(expectedProduct);
    }

    private async Task UpdateProductDetails(string id, object newProductDetails)
    {
        await Api.Admin
            .EditProductAsync(id, newProductDetails)
            .EnsureIsSuccess();
    }

    private async Task CheckProductDetails(ReadProductTestModel expectedProduct)
    {
        var productDetails = await Api.Admin
            .FindProductAsync(expectedProduct.Id)
            .EnsureIsSuccess();

        await productDetails
            .Should()
            .ContainContentAsync(expectedProduct);
    }
}