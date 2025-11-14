namespace Store.Core.Business.Orders
{
    public sealed record GetCustomerOrdersQuery : IRequest<IEnumerable<OrderSummaryModel>>;

    internal sealed class GetCustomerOrdersQueryHandler
        : IRequestHandler<GetCustomerOrdersQuery, IEnumerable<OrderSummaryModel>>
    {
        public Task<IEnumerable<OrderSummaryModel>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
