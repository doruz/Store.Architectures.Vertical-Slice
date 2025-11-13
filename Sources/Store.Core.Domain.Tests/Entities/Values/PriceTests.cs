using FluentAssertions;
using Store.Core.Domain.Entities;

namespace Store.Core.Domain.Tests.Entities.Values;

public class PriceTests
{
    [Fact]
    public void When_PriceValueIsNegative_Should_ThrowException()
    {
        // Arrange & Act
        Action result = () => new Price(-1);

        // Assert
        result.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(99.99)]
    [InlineData(99.959599)]
    public void When_PriceIsCreated_Should_ContainCorrectValue(decimal value)
    {
        // Arrange & Act
        Price price1 = value;
        Price price2 = new Price(value);

        // Assert
        price1.Value.Should().Be(value);
        price2.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1.5, 2, 3)]
    [InlineData(9.99, 2, 19.98)]
    [InlineData(9.99, 5, 49.95)]
    [InlineData(9.9595, 7, 69.7165)]
    public void When_PriceIsMultiplied_Should_ContainCorrectValue(decimal value, int quantity, decimal expectedValue)
    {
        // Arrange
        Price price = new Price(value);

        // Act
        Price result = price * quantity;

        // Assert
        result.Value.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(2, 3, 5)]
    [InlineData(1.5, 2, 3.5)]
    [InlineData(9.99, 1.01, 11)]
    [InlineData(9.99, 5.25, 15.24)]
    [InlineData(9.9595, 5.25, 15.2095)]
    public void When_PriceIsAdded_Should_ContainCorrectValue(decimal price1Value, decimal price2Value, decimal expectedValue)
    {
        // Arrange
        Price price1 = price1Value;
        Price price2 = price2Value;

        // Act
        Price result = price1 + price2;

        // Assert
        result.Value.Should().Be(expectedValue);
    }

    [Fact]
    public void When_PricesAreSummed_Should_ContainCorrectValue()
    {
        // Arrange
        Price[] prices = [1, 2.5m, 0, 0.5m, 9.99m, 9.99m, 1.01m, 5.25m, 4.9998m, 15.1234m];

        // Act
        Price result = prices.Sum();

        // Assert
        result.Value.Should().Be(50.3632m);
    }
}