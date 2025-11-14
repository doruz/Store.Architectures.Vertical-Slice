using Microsoft.AspNetCore.Mvc.Filters;
using Store.Core.Business.Shared;

/// <summary>
/// In case a specific app error is thrown to return correct status code to the client.
/// </summary>
internal sealed class BusinessExceptionsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BusinessException exception)
        {
            context.Result = new ObjectResult(exception.Error)
            {
                StatusCode = exception.StatusCode
            };

            context.ExceptionHandled = true;
        }
    }
}