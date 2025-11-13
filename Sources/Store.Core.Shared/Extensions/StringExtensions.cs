namespace Store.Core.Shared;

public static class StringExtensions
{
    public static bool IsEqualTo(this string? actual, string? expected)
        => actual?.ToLower() == expected?.ToLower();

    public static bool IsNotEqualTo(this string? actual, string? expected)
        => !actual.IsEqualTo(expected);
}