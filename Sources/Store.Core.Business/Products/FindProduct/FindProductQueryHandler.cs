using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.Products;

internal sealed class FindProductQueryHandler(RepositoriesContext repositories)
    : IRequestHandler<FindProductQuery, ProductModel>
{
    public async Task<ProductModel> Handle(FindProductQuery query, CancellationToken _) =>
        await repositories.Products
            .FindAsync(query.Id)
            .EnsureIsNotNull(query.Id)
            .MapAsync(ProductModel.Create);
}