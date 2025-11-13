using Store.Core.Business.Products;

namespace Store.Core.Business.Customers.Products;

public sealed record FindProductQuery(string Id) : IRequest<ProductModel>;