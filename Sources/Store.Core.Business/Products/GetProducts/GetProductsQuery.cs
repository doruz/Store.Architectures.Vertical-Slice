using Store.Core.Domain.Entities;

namespace Store.Core.Business.Products;

public sealed record GetProductsQuery : IRequest<IEnumerable<GetProductModel>>
{
    internal Func<Product, bool> Filter { get; }

    private GetProductsQuery(Func<Product, bool> filter) => Filter = filter;

    public static GetProductsQuery Available() => new(product => product is { Stock: > 0, DeletedAt: null });
    public static GetProductsQuery All() => new(product => product.DeletedAt == null);
}