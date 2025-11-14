namespace Store.Core.Business.Products;

public sealed record GetAvailableProductsQuery : IRequest<IEnumerable<ProductModel>>;