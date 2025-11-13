namespace Store.Core.Shared;

public static class ValidationMessages
{
    public const string Required = "Field is required.";

    public const string Range = "Value must be between {1} and {2}.";

    public const string MinValue = "Value must be greater or equal than {1}.";

    public const string MaxLength = "Value must have maximum length {1}.";
}