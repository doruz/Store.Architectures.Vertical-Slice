using Store.Core.Domain.Repositories;

namespace Store.Core.Business.Products;

internal sealed class GetProductsQueryHandler(RepositoriesContext repositories)
    : IRequestHandler<GetProductsQuery, IEnumerable<ProductModel>>
{
    public async Task<IEnumerable<ProductModel>> Handle(GetProductsQuery query, CancellationToken _)
    {
        var products = await repositories.Products.GetAsync(query.Filter);

        return products
            .OrderBy(p => p.Name, StringComparer.InvariantCultureIgnoreCase)
            .Select(ProductModel.Create);
    }
}