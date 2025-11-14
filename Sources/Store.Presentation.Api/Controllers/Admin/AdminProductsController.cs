using Store.Core.Business.Products;
using Store.Core.Business.Shared;

[ApiRoute("admins/products")]
public sealed class AdminProductsController(IMediator mediator) : BaseApiController(mediator)
{
    /// <summary>
    /// Get details of all existing products.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IEnumerable<GetProductModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProducts() 
        => await HandleQuery(GetProductsQuery.All());

    /// <summary>
    /// Find details of a specific product.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType<GetProductModel>(StatusCodes.Status200OK)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindProduct([FromRoute] FindProductQuery query) 
        => await HandleQuery(query);

    /// <summary>
    /// Add new product details.
    /// </summary>
    [HttpPost]
    [ProducesResponseType<IdModel>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command)
    {
        IdModel newProduct = await Handle(command);

        return CreatedAtAction(nameof(FindProduct), new { newProduct.Id }, newProduct);
    }

    /// <summary>
    /// Update details of an existing product.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] UpdateProductCommand command)
        => await HandleCommand(command.For(id));

    /// <summary>
    /// Remove details of an existing product.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductCommand command)
        => await HandleCommand(command);
}