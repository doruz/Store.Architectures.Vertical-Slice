using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.Products;
using Store.Core.Business.Shared;

[ApiRoute("customers/current/products")]
public sealed class CustomersProductsController(ProductsService products) : BaseApiController
{
    /// <summary>
    /// Get details of all available products.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IEnumerable<ProductModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailableProducts()
        => Ok(await products.GetAllAvailable());

    /// <summary>
    /// Find details of a specific product.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType<ProductModel>(StatusCodes.Status200OK)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindProduct([FromRoute] string id)
        => Ok(await products.FindProductAsync(id));
}