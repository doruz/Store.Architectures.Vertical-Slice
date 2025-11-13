using Store.Core.Business.Products;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Core.Business.Customers.Products;

internal sealed class GetAvailableProductsQueryHandler(RepositoriesContext repositories)
    : IRequestHandler<GetAvailableProductsQuery, IEnumerable<ProductModel>>
{
    public async Task<IEnumerable<ProductModel>> Handle(GetAvailableProductsQuery request, CancellationToken _)
    {
        var products = await repositories.Products.FilterAsync(Product.IsAvailable());

        return products
            .OrderBy(p => p.Name, StringComparer.InvariantCultureIgnoreCase)
            .Select(ProductsMapper.ToProductModel);
    }
}