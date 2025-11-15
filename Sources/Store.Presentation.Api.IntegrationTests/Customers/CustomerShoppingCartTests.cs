namespace Store.Presentation.Api.IntegrationTests.Customers;

public class CustomerShoppingCartTests(ApiApplicationFactory factory) : ApiBaseTests(factory)
{
    private static readonly ReadShoppingCartTestModel EmptyCart = new()
    {
        Lines = [],
        TotalPrice = new PriceTestModel(0)
    };

    private static readonly ReadShoppingCartTestModel TestCart = new()
    {
        Lines =
        [
            new ReadShoppingCartLineTestModel
            {
                ProductId = "9b5055cf-6cd0-4086-8d01-6e1582a7fb0a",
                ProductName = "Apples",
                ProductPrice = new PriceTestModel(0.99m),
                Quantity = 2,
                TotalPrice = new PriceTestModel(1.98m)
            },
            new ReadShoppingCartLineTestModel
            {
                ProductId = "b4f256a5-f65f-4811-a0d4-10d1fbba5f25",
                ProductName = "Bananas",
                ProductPrice = new PriceTestModel(0.75m),
                Quantity = 3,
                TotalPrice = new PriceTestModel(2.25m)
            }
        ],

        TotalPrice = new PriceTestModel(4.23m)
    };

    [Fact]
    public async Task When_CartIsCleared_Should_AlwaysReturnSuccess()
    {
        // Act
        var responses = await Api.ExecuteTwice(api => api.Customer.Cart.ClearAsync());

        // Assert
        responses.Select(r => r.StatusCode)
            .Should()
            .AllBeEquivalentTo(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task When_CartIsEmpty_Should_ReturnDefaultDetails()
    {
        // Arrange
        await ClearCart();

        // Act & Assert
        await CartShouldBe(EmptyCart);
    }

    [Fact]
    public async Task When_UpdatingCartWithUnknownProduct_Should_ReturnNotFound()
    {
        // Act
        var response = await Api.Customer.Cart
            .UpdateAsync(cart => cart.With(TestProducts.UnknownId, 1));

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.NotFound)
            .And.ContainContentAsync(new AppErrorTestModel("product_not_found"));
    }

    [Fact]
    public async Task When_UpdatingCartWithUnavailableStock_Should_ReturnConflict()
    {
        // Act
        var response = await Api.Customer.Cart
            .UpdateAsync(cart => cart.Apples(TestProducts.Apples.Stock + 1));

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.Conflict)
            .And.ContainContentAsync(new AppErrorTestModel("product_stock_not_available"));
    }

    [Fact]
    public async Task When_UpdatingCartWithDifferentProductsInSameRequest_Should_AllBeAdded()
    {
        // Arrange
        await ClearCart();

        // Act
        await UpdateCart(cart => cart.Apples(2).Bananas(3));

        // Assert
        await CartShouldBe(TestCart);
    }

    [Fact]
    public async Task When_UpdatingCartWithDifferentProductsIndividualRequest_Should_AllBeAdded()
    {
        // Arrange
        await ClearCart();

        // Act
        await UpdateCart(cart => cart.Apples(2));
        await UpdateCart(cart => cart.Bananas(3));

        // Assert
        await CartShouldBe(TestCart);
    }

    [Fact]
    public async Task When_UpdatingCartWithSameProductsInSameRequest_Should_GroupSameProducts()
    {
        // Arrange
        await ClearCart();

        // Act
        await UpdateCart(cart => cart
            .Apples(1)
            .Apples(1)
            .Bananas(1)
            .Bananas(2)
        );

        // Assert
        await CartShouldBe(TestCart);
    }

    [Fact]
    public async Task When_UpdatingCartWithSameProductsInDifferentRequests_Should_OverwriteProductsQuantities()
    {
        // Arrange
        await ClearCart();
        
        // Act
        await UpdateCart(cart => cart.Apples(1).Bananas(1));
        await UpdateCart(cart => cart.Apples(2).Bananas(3));

        // Assert
        await CartShouldBe(TestCart);
    }

    [Fact]
    public async Task When_UpdatingCartProductsWithZeroQuantity_Should_RemoveThemFromCart()
    {
        // Arrange
        await ClearCart();

        // Act
        await UpdateCart(cart => cart.Apples(1).Bananas(1));
        await UpdateCart(cart => cart.Apples(0).Bananas(0));

        // Assert
        await CartShouldBe(EmptyCart);
    }

    [Fact]
    public async Task When_TryingToRemoveProductWhichIsNotAddedInCart_ShouldNotThrowError()
    {
        // Arrange
        await ClearCart();

        // Act
        await UpdateCart(cart => cart.Apples(1));
        await UpdateCart(cart => cart.Apples(0).Bananas(0));

        // Assert
        await CartShouldBe(EmptyCart);
    }

    [Fact]
    public async Task When_EmptyCartIsCheckout_Should_ReturnError()
    {
        // Arrange
        await ClearCart();

        // Act
        var response = await Api.Customer.Cart.CheckoutAsync();

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.NotFound)
            .And.ContainContentAsync(new AppErrorTestModel("shopping_cart_is_empty"));
    }

    [Fact]
    public async Task When_NonEmptyCartIsCheckout_Should_ReturnSuccess()
    {
        // Arrange
        await ClearCart();
        await UpdateCart(cart => cart.Apples(1));

        // Act
        var response = await Api.Customer.Cart.CheckoutAsync();

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.Created)
            .And.ContainIdAsync();
    }

    [Fact]
    public async Task When_NonEmptyCartIsCheckout_Should_ClearCurrentCart()
    {
        // Arrange
        await ClearCart();
        await UpdateCart(cart => cart.Apples(1));

        // Act
        await Api.Customer.Cart.CheckoutAsync().EnsureIsSuccess();

        // Assert
        await CartShouldBe(EmptyCart);
    }

    [Fact]
    public async Task When_NonEmptyCartIsCheckout_Should_DecreaseProductsStocks()
    {
        // Arrange
        await ClearCart();
        await UpdateCart(cart => cart.Apples(4).Bananas(5));

        // Act
        await Api.Customer.Cart.CheckoutAsync().EnsureIsSuccess();

        // Assert
        await ProductShouldHaveStock(TestProducts.Apples.Id, 1);
        await ProductShouldHaveStock(TestProducts.Bananas.Id, 5);
    }

    private Task ClearCart() =>
        Api.Customer.Cart
            .ClearAsync()
            .EnsureIsSuccess();

    private Task UpdateCart(Action<UpdateShoppingCartTestModel> shoppingCartActions) =>
        Api.Customer.Cart
            .UpdateAsync(shoppingCartActions)
            .EnsureIsSuccess();

    private async Task CartShouldBe(ReadShoppingCartTestModel expectedCart)
    {
        var response = await Api.Customer.Cart.GetAsync();

        await response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContainContentAsync(expectedCart);
    }

    private async Task ProductShouldHaveStock(string id, int expectedStock)
    {
        var product = await Api.Customer.Products
            .FindProductAsync(id)
            .EnsureIsSuccess()
            .ContentAsAsync<ReadProductTestModel>();

        product.Stock.Should().Be(expectedStock);
    }
}