using Store.Core.Business.Products;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.Customers.Products;

internal sealed class FindProductQueryHandler(RepositoriesContext repositories)
    : IRequestHandler<FindProductQuery, ProductModel>
{
    public async Task<ProductModel> Handle(FindProductQuery request, CancellationToken _)
    {
        return await repositories.Products
            .FindAsync(request.Id)
            .EnsureIsNotNull(request.Id)
            .MapAsync(ProductsMapper.ToProductModel);
    }
}