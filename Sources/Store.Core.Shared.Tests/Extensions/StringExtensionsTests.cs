using FluentAssertions;

namespace Store.Core.Shared.Tests.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData(null, null, true)]
    [InlineData(null, "", false)]
    [InlineData("", null, false)]
    [InlineData("", "", true)]
    [InlineData("hello", "hello", true)]
    [InlineData("heLLo", "HEllO", true)]
    [InlineData("HEllO", "heLLo", true)]
    [InlineData("hello ", "hello", false)]
    [InlineData("hello", " hello", false)]
    public void When_CheckingIfStringsAreEqual_Should_VerifyAsInvariantStrings(string? first, string? second, bool expectedValue)
    {
        first.IsEqualTo(second).Should().Be(expectedValue);
    }
}