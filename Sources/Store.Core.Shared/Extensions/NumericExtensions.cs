namespace Store.Core.Shared;

public static class NumericExtensions
{
    public static bool IsInRange(this int value, int min, int max)
        => value >= min && value <= max;

    public static bool IsNotInRange(this int value, int min, int max)
        => !value.IsInRange(min, max);
}