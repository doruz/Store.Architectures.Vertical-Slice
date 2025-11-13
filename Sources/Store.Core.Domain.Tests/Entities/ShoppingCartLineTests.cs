using FluentAssertions;
using Store.Core.Domain.Entities;

namespace Store.Core.Domain.Tests.Entities;

public class ShoppingCartLineTests
{
    private const string ValidProductId = "test";
    private const int ValidQuantity = 1;

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    public void When_InstanceIsCreatedWithNullOrEmptyProductId_Should_ThrowException(string productId)
    {
        // Arrange & Act
        var action = () => new ShoppingCartLine(productId, ValidQuantity);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void When_InstanceIsCreatedWithNegativeQuantity_Should_ThrowException()
    {
        // Arrange & Act
        var action = () => new ShoppingCartLine(ValidProductId, -1);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void When_InstanceIsCreated_Should_ContainProvidedValues()
    {
        // Arrange & Act
        ShoppingCartLine systemUnderTest = new(ValidProductId, ValidQuantity);

        // Assert
        systemUnderTest.ProductId.Should().Be(ValidProductId);
        systemUnderTest.Quantity.Should().Be(ValidQuantity);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 5)]
    public void When_QuantityIsUpdated_Should_ContainCorrectValues(int initialQuantity, int newQuantity)
    {
        // Arrange
        ShoppingCartLine systemUnderTest = new(ValidProductId, initialQuantity);

        // Act
        ShoppingCartLine result = systemUnderTest.WithQuantity(newQuantity);

        // Assert
        result.Should().NotBe(systemUnderTest);
        result.ProductId.Should().Be(ValidProductId);
        result.Quantity.Should().Be(newQuantity);
    }

    [Theory]
    [InlineData(1, 6, 7)]
    [InlineData(3, 7, 10)]
    [InlineData(3, -1, 2)]
    [InlineData(3, -3, 0)]
    public void When_QuantityIsIncreased_Should_ContainCorrectValues(int initialQuantity, int extraQuantity, int newQuantity)
    {
        // Arrange
        ShoppingCartLine systemUnderTest = new(ValidProductId, initialQuantity);

        // Act
        ShoppingCartLine result = systemUnderTest.IncreaseQuantity(extraQuantity);

        // Assert
        result.Should().NotBe(systemUnderTest);
        result.ProductId.Should().Be(ValidProductId);
        result.Quantity.Should().Be(newQuantity);
    }
}