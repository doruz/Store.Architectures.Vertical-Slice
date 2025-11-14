[ApiController]
[Produces("application/json")]
public abstract class BaseApiController(IMediator mediator = null) : ControllerBase
{
    // TODO: to change mediator to not be null

    protected async Task<TResponse> Handle<TResponse>(IRequest<TResponse> request)
        => await mediator.Send(request);

    protected async Task<IActionResult> HandleCommand(IRequest command)
    {
        await mediator.Send(command);

        return NoContent();
    }

    protected async Task<IActionResult> HandleQuery<TResponse>(IRequest<TResponse> query)
        => Ok(await mediator.Send(query));
}