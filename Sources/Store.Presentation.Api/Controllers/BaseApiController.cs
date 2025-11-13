using MediatR;

[ApiController]
[Produces("application/json")]
public abstract class BaseApiController(IMediator mediator = null) : ControllerBase
{
    // TODO: to change mediator to not be null
    protected IActionResult OkOrNotFound<T>(T? value) =>
        value is null
            ? NotFound()
            : Ok(value);

    protected async Task<IActionResult> Execute<TResponse>(IRequest<TResponse> request)
        => Ok(await mediator.Send(request));
}