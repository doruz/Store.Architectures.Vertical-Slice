using FluentAssertions;

namespace Store.Core.Shared.Tests.Extensions;

public class DateTimeExtensionsTests
{
    [Fact]
    public void When_DateIsFormatted_Should_ReturnCorrectDateTimeString()
    {
        // Arrange
        var date = new DateTime(2025, 11, 4, 09, 30, 55);

        // Act & Assert
        date.ToDateTimeString().Should().Be("04 Nov 2025, 09:30");
    }
}