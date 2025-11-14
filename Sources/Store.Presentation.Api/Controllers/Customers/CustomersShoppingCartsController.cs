using Store.Core.Business.Orders;
using Store.Core.Business.Shared;
using Store.Core.Business.ShoppingCarts;

[ApiRoute("customers/current/shopping-carts/current")]
public sealed class CustomersShoppingCartsController(
    ShoppingCartsService shoppingCarts,
    ShoppingCartCheckoutService shoppingCartCheckout,
    IMediator mediator) : BaseApiController(mediator)
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
    {
        await shoppingCarts.ClearCurrentCustomerCart();

        return NoContent();
    }

    /// <summary>
    /// Update cart details of authenticated customer.
    /// </summary>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateCurrentCart([FromBody] EditShoppingCartLineModel[] lines)
    {
        await shoppingCarts.UpdateCurrentCustomerCart(lines);

        return NoContent();
    }

    /// <summary>
    /// Make an order from current cart.
    /// </summary>
    [HttpPost("checkout")]
    [ProducesResponseType<OrderSummaryModel>(StatusCodes.Status201Created)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CheckoutCart()
    {
        OrderSummaryModel orderSummary = await shoppingCartCheckout.CheckoutCurrentCustomerCart();

        return CreatedAtRoute("OrderDetails", new { OrderId = orderSummary.Id }, orderSummary);
    }
}