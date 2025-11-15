using Store.Core.Business.Orders;
using Store.Core.Shared;

[ApiRoute("customers/current/orders")]
public sealed class CustomersOrdersController(IMediator mediator) : BaseApiController(mediator)
{
    /// <summary>
    /// Get summaries of all orders made by the authenticated customer.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IEnumerable<OrderSummaryModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersSummary()
        => await HandleQuery(new GetCustomerOrdersQuery());

    /// <summary>
    /// Find details of a specific order made by the authenticated customer.
    /// </summary>
    [HttpGet("{orderId}", Name = "OrderDetails")]
    [ProducesResponseType<FindCustomerOrderQueryResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<AppErrorModel>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindOrderDetails([FromRoute] FindCustomerOrderQuery query)
        => await HandleQuery(query);
}