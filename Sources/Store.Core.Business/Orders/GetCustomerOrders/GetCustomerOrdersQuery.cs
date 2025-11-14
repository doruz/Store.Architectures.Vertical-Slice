namespace Store.Core.Business.Orders;

public sealed record GetCustomerOrdersQuery : IRequest<IEnumerable<OrderSummaryModel>>;
