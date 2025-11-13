using Store.Core.Business.Products;

namespace Store.Core.Business.Customers.Products;

public sealed record GetAvailableProductsQuery : IRequest<IEnumerable<ProductModel>>;