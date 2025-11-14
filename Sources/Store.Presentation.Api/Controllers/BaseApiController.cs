[ApiController]
[Produces("application/json")]
public abstract class BaseApiController(IMediator mediator = null) : ControllerBase
{
    // TODO: to change mediator to not be null

    protected async Task<IActionResult> Handle<TResponse>(IRequest<TResponse> request)
        => Ok(await mediator.Send(request));
}