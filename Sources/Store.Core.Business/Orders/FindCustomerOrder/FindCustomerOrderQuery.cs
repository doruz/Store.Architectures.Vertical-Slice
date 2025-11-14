namespace Store.Core.Business.Orders;

public sealed record FindCustomerOrderQuery(string OrderId): IRequest<FindCustomerOrderQueryResult>;