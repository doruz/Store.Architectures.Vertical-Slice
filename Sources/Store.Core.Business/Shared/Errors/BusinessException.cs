namespace Store.Core.Business.Shared;

public sealed class BusinessException(BusinessError error) : Exception
{
    public BusinessError Error { get; } = error;

    public int StatusCode => Error.Status;
}