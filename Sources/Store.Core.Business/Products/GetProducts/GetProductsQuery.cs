using Store.Core.Domain.Entities;

namespace Store.Core.Business.Products;

public sealed record GetProductsQuery : IRequest<IEnumerable<ProductModel>>
{
    internal Func<Product, bool> Filter { get; }

    private GetProductsQuery(Func<Product, bool> filter) => Filter = filter;

    public static GetProductsQuery Available() => new(product => product.Stock > 0);
    public static GetProductsQuery All() => new(_ => true);
}