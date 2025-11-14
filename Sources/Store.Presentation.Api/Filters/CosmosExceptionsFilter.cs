using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Azure.Cosmos;
using System.Net;

/// <summary>
/// In case Cosmos DB throws a NotFound or Conflict exception to return status code to the client.
/// </summary>
internal sealed class CosmosExceptionsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CosmosException { StatusCode: HttpStatusCode.NotFound })
        {
            context.Result = new NotFoundResult();
            context.ExceptionHandled = true;
        }

        if (context.Exception is CosmosException { StatusCode: HttpStatusCode.Conflict })
        {
            context.Result = new ConflictResult();
            context.ExceptionHandled = true;
        }
    }
}