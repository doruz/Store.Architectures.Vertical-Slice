using Store.Core.Domain.Repositories;

namespace Store.Core.Business.Products;

internal sealed class GetProductsQueryHandler(RepositoriesContext repositories)
    : IRequestHandler<GetProductsQuery, IEnumerable<ProductModel>>
{
    public async Task<IEnumerable<ProductModel>> Handle(GetProductsQuery request, CancellationToken _)
    {
        var products = await repositories.Products.GetAsync(request.Filter);

        return products
            .OrderBy(p => p.Name, StringComparer.InvariantCultureIgnoreCase)
            .Select(ProductModel.Create);
    }
}