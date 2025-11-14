using FluentAssertions;

namespace Store.Core.Shared.Tests.Extensions;

public class NumericExtensionsTests
{
    [Theory]
    [InlineData(0, false)]
    [InlineData(1, true)]
    [InlineData(3, true)]
    [InlineData(5, true)]
    [InlineData(6, false)]
    public void When_CheckingIfNumberIsInRange_Should_ReturnCorrectValue(int number, bool expectedValue)
    {
        number.IsInRange(1, 5).Should().Be(expectedValue);
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(1, false)]
    [InlineData(3, false)]
    [InlineData(5, false)]
    [InlineData(6, true)]
    public void When_CheckingIfNumberIsNotInRange_Should_ReturnCorrectValue(int number, bool expectedValue)
    {
        number.IsNotInRange(1, 5).Should().Be(expectedValue);
    }
}