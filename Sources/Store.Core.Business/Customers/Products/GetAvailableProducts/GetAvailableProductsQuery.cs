namespace Store.Core.Business.Customers.Products;

public sealed record GetAvailableProductsQuery : IRequest<IEnumerable<ProductModel>>;