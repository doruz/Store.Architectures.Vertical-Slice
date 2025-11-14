using Store.Core.Business.Products;
using Store.Core.Business.Shared;

[ApiRoute("admins/products")]
public sealed class AdminProductsController(ProductsService products, IMediator mediator) : BaseApiController(mediator)
{
    /// <summary>
    /// Get details of all existing products.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IEnumerable<ProductModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProducts() 
        => await HandleQuery(GetProductsQuery.All());

    /// <summary>
    /// Find details of a specific product.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType<ProductModel>(StatusCodes.Status200OK)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindProduct([FromRoute] FindProductQuery query) 
        => await HandleQuery(query);

    /// <summary>
    /// Add new product details.
    /// </summary>
    [HttpPost]
    [ProducesResponseType<ProductModel>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddProduct([FromBody] NewProductModel model)
    {
        var newProduct = await products.AddAsync(model);

        return CreatedAtAction(nameof(FindProduct), new { newProduct.Id }, newProduct);
    }

    /// <summary>
    /// Update details of an existing product.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] EditProductModel model)
    {
        await products.UpdateAsync(id, model);

        return NoContent();
    }

    /// <summary>
    /// Remove details of an existing product.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] string id)
    {
        await products.DeleteAsync(id);

        return NoContent();
    }
}