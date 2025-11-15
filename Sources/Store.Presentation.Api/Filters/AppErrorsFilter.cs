using Microsoft.AspNetCore.Mvc.Filters;
using Store.Core.Shared;

/// <summary>
/// In case a specific app error is thrown to return correct status code to the client.
/// </summary>
internal sealed class AppErrorsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is AppError error)
        {
            context.Result = new ObjectResult(CreateResult(error))
            {
                StatusCode = error.StatusCode
            };

            context.ExceptionHandled = true;
        }
    }

    private static AppErrorModel CreateResult(AppError error)
        => new(error.Error, error.ErrorDetails);
}