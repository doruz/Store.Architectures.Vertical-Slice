using Store.Core.Domain.Repositories;

namespace Store.Core.Business.Products;

internal sealed class GetProductsQueryHandler(RepositoriesContext repositories)
    : IRequestHandler<GetProductsQuery, IEnumerable<GetProductModel>>
{
    public async Task<IEnumerable<GetProductModel>> Handle(GetProductsQuery query, CancellationToken _)
    {
        var products = await repositories.Products.GetAsync(query.Filter);

        return products
            .OrderBy(p => p.Name, StringComparer.InvariantCultureIgnoreCase)
            .Select(GetProductModel.Create);
    }
}