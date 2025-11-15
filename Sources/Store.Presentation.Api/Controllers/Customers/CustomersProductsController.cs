using Store.Core.Business.Products;
using Store.Core.Shared;

[ApiRoute("customers/current/products")]
public sealed class CustomersProductsController(IMediator mediator) : BaseApiController(mediator)
{
    /// <summary>
    /// Get details of all available products.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IEnumerable<GetProductModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailableProducts()
        => await HandleQuery(GetProductsQuery.Available());

    /// <summary>
    /// Find details of a specific product.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType<GetProductModel>(StatusCodes.Status200OK)]
    [ProducesResponseType<AppErrorModel>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindProduct([FromRoute] FindProductQuery query)
        => await HandleQuery(query);
}