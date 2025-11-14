using Store.Core.Business.Products;
using Store.Core.Business.Shared;

[ApiRoute("customers/current/products")]
public sealed class CustomersProductsController(IMediator mediator) : BaseApiController(mediator)
{
    /// <summary>
    /// Get details of all available products.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IEnumerable<ProductModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailableProducts()
        => await HandleQuery(GetProductsQuery.Available());

    /// <summary>
    /// Find details of a specific product.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType<ProductModel>(StatusCodes.Status200OK)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindProduct([FromRoute] FindProductQuery query)
        => await HandleQuery(query);
}