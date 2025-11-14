using Store.Core.Business.Orders;
using Store.Core.Business.Shared;

[ApiRoute("customers/current/orders")]
public sealed class CustomersOrdersController(OrdersService orders) : BaseApiController
{
    /// <summary>
    /// Get summaries of all orders made by the authenticated customer.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IEnumerable<OrderSummaryModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersSummary()
        => Ok(await orders.GetCurrentCustomerOrders());

    /// <summary>
    /// Find details of a specific order made by the authenticated customer.
    /// </summary>
    [HttpGet("{orderId}", Name = "OrderDetails")]
    [ProducesResponseType<OrderDetailedModel>(StatusCodes.Status200OK)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindOrderDetails([FromRoute] string orderId)
        => Ok(await orders.FindCurrentCustomerOrder(orderId));
}