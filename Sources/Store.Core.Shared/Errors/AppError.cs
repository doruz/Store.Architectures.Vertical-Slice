namespace Store.Core.Shared;

public sealed class AppError(int statusCode, string error) : Exception
{
    public int StatusCode { get; } = statusCode;
    public string Error { get; } = error;
    public object ErrorDetails { get; private init; } = new { };

    public static AppError NotFound(string error) => new(404, error);
    public static AppError NotFound(string error, string id) => new(404, error)
    {
        ErrorDetails = new { id }
    };

    public static AppError Conflict(string error, string id) => new(409, error)
    {
        ErrorDetails = new { id }
    };
}