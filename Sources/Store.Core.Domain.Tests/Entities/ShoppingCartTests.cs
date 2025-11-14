using FluentAssertions;
using Store.Core.Domain.Entities;

namespace Store.Core.Domain.Tests.Entities;

public class ShoppingCartTests
{
    private static readonly ShoppingCartLine CartLine = new("4", 1);
    private static readonly ShoppingCartLine[] CartLines =
    [
        new ShoppingCartLine("1", 1),
        new ShoppingCartLine("2", 2),
        new ShoppingCartLine("3", 3),
    ];

    private readonly ShoppingCart _systemUnderTest = ShoppingCart.Empty("1234");

    [Fact]
    public void When_ProductIsNew_Should_BeAdded()
    {
        // Act
        _systemUnderTest.UpdateOrRemoveLine(CartLine);

        // Assert
        _systemUnderTest.Lines.Should().HaveCount(1);
        _systemUnderTest.Lines.Should().Contain(CartLine);
    }

    [Fact]
    public void When_ProductExists_Should_BeUpdated()
    {
        // Arrange
        ShoppingCartLine updatedCartLine = CartLine.IncreaseQuantity(2);

        // Act
        _systemUnderTest.UpdateOrRemoveLine(CartLine);
        _systemUnderTest.UpdateOrRemoveLine(updatedCartLine);

        // Assert
        _systemUnderTest.Lines.Should().HaveCount(1);
        _systemUnderTest.Lines.Should().Contain(updatedCartLine);
    }

    [Fact]
    public void When_ProductQuantityIsZero_Should_BeRemoved()
    {
        // Arrange
        ShoppingCartLine newCartLine = new("1", 1);
        ShoppingCartLine updatedCartLine = new("1", 0);

        // Act
        _systemUnderTest.UpdateOrRemoveLine(newCartLine);
        _systemUnderTest.UpdateOrRemoveLine(updatedCartLine);

        // Assert
        _systemUnderTest.Lines.Should().BeEmpty();
    }

    [Fact]
    public void When_ProductsAreNew_Should_AllBeAdded()
    {
        // Act
        _systemUnderTest.UpdateOrRemoveLines(CartLines);

        // Assert
        _systemUnderTest.Lines.Should().BeEquivalentTo(CartLines);
    }

    [Fact]
    public void When_ProductsHaveZeroQuantity_Should_BeRemoved()
    {
        // Arrange
        ShoppingCartLine[] updatedCartLines =
        [
            CartLines[0].IncreaseQuantity(3),
            CartLines[1].IncreaseQuantity(5),
            CartLines[2].WithQuantity(0)
        ];

        // Act
        _systemUnderTest.UpdateOrRemoveLines(updatedCartLines);

        // Assert
        _systemUnderTest.Lines.Should().BeEquivalentTo(updatedCartLines.Take(2));
    }

    [Fact]
    public void When_SameProductsAreUpdated_Should_BeMerged()
    {
        // Arrange
        ShoppingCartLine expectedCartLine = CartLine.WithQuantity(5);

        ShoppingCartLine[] updatedCartLines =
        [
            CartLine.WithQuantity(0),
            CartLine.WithQuantity(2),
            CartLine.WithQuantity(3),
        ];

        // Act
        _systemUnderTest.UpdateOrRemoveLines(updatedCartLines);

        // Assert
        _systemUnderTest.Lines.Should().BeEquivalentTo([expectedCartLine]);
    }
}