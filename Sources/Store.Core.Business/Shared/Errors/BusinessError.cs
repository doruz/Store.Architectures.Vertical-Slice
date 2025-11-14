namespace Store.Core.Business.Shared;

public record BusinessError(int Status, string Error)
{
    public object ErrorDetails { get; private set; } = new { };

    public static BusinessError NotFound(string error) => new(404, error);

    public static BusinessError NotFound(string error, string id) => new(404, error)
    {
        ErrorDetails = new { id }
    };

    public static BusinessError Conflict(string error, string id) => new(409, error)
    {
        ErrorDetails = new { id }
    };
}