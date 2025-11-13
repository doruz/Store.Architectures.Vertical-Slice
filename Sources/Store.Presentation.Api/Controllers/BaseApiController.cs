using Microsoft.AspNetCore.Mvc;

[ApiController]
[Produces("application/json")]
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult OkOrNotFound<T>(T? value) => 
        value is null
            ? NotFound()
            : Ok(value);


}
