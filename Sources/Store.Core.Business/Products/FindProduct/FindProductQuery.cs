namespace Store.Core.Business.Products;

public sealed record FindProductQuery(string Id) : IRequest<GetProductModel>;