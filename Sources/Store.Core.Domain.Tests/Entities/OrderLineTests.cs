using FluentAssertions;
using Store.Core.Domain.Entities;

namespace Store.Core.Domain.Tests.Entities;

public class OrderLineTests
{
    private static readonly Product Product = Products.Second;

    [Fact]
    public void When_OrderLineIsCreated_Should_ThrowExceptionWhenProductDoesNotMatch()
    {
        // Arrange & Act
        var action = () =>
        {
            OrderLine.Create(new ShoppingCartLine("1", 1), Product);
        };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void When_OrderLineIsCreated_Should_ThrowExceptionWhenQuantityIsNegative()
    {
        // Arrange & Act
        var action = () =>
        {
            OrderLine.Create(new ShoppingCartLine(Product.Id, -1), Product);
        };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void When_OrderLineIsCreated_Should_ThrowExceptionWhenStockIsNotAvailable()
    {
        // Arrange & Act
        var action = () =>
        {
            OrderLine.Create(new ShoppingCartLine(Product.Id, Product.Stock + 1), Product);
        };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void When_OrderLineIsCreated_Should_CopyProductDetails()
    {
        // Arrange & Act
        var systemUnderTest = OrderLine.Create(new ShoppingCartLine(Product.Id, 3), Product);

        // Assert
        systemUnderTest.ProductId.Should().Be(Product.Id);
        systemUnderTest.ProductName.Should().Be(Product.Name);
        systemUnderTest.ProductPrice.Should().Be(Product.Price);
    }

    [Fact]
    public void When_OrderLineIsCreated_Should_CopyCartLineDetails()
    {
        // Arrange & Act
        var systemUnderTest = OrderLine.Create(new ShoppingCartLine(Product.Id, 3), Product);

        // Assert
        systemUnderTest.Quantity.Should().Be(3);
    }

    [Fact]
    public void When_OrderLineIsCreated_Should_ContainCorrectTotalPrice()
    {
        // Arrange & Act
        var systemUnderTest = OrderLine.Create(new ShoppingCartLine(Product.Id, 3), Product);

        // Assert
        systemUnderTest.TotalPrice.Should().Be(new Price(2.97m));
    }
}