using Store.Core.Business.Shared;
using Store.Core.Business.ShoppingCarts;

[ApiRoute("customers/current/shopping-carts/current")]
public sealed class CustomersShoppingCartsController(IMediator mediator) : BaseApiController(mediator)
{
    /// <summary>
    /// Get cart details of authenticated customer.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<GetCustomerCartQueryResult>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentCart() 
        => await HandleQuery(new GetCustomerCartQuery());

    /// <summary>
    /// Clear cart details of authenticated customer.
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> ClearCurrentCart()
        => await HandleCommand(new ClearCustomerCartCommand());

    /// <summary>
    /// Update cart details of authenticated customer.
    /// </summary>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateCurrentCart([FromBody] UpdateCustomerCartLineModel[] lines)
        => await HandleCommand(new UpdateCustomerCartCommand(lines));

    /// <summary>
    /// Make an order from current cart.
    /// </summary>
    [HttpPost("checkout")]
    [ProducesResponseType<IdModel>(StatusCodes.Status201Created)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CheckoutCart()
    {
        IdModel order = await Handle(new CheckoutCustomerCartCommand());

        return CreatedAtRoute("OrderDetails", new { OrderId = order.Id }, order);
    }
}