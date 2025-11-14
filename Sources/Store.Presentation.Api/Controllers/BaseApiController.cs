[ApiController]
[Produces("application/json")]
public abstract class BaseApiController(IMediator mediator = null) : ControllerBase
{
    // TODO: to change mediator to not be null

    protected async Task<TResponse> Handle<TResponse>(IRequest<TResponse> request)
        => await mediator.Send(request);

    protected async Task<IActionResult> HandleQuery<TResponse>(IRequest<TResponse> request)
        => Ok(await mediator.Send(request));
}