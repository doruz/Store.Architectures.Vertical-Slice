using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Produces("application/json")]
public abstract class BaseApiController(IMediator mediator = null) : ControllerBase
{
    // TODO: to change mediator to not be null
    protected IActionResult OkOrNotFound<T>(T? value) =>
        value is null
            ? NotFound()
            : Ok(value);

    protected async Task<IActionResult> ProcessQuery<TResponse>(IRequest<TResponse> query)
        => Ok(await mediator.Send(query));
}