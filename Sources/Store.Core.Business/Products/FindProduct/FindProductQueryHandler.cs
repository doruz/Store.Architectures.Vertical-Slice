using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.Products;

internal sealed class FindProductQueryHandler(RepositoriesContext repositories)
    : IRequestHandler<FindProductQuery, GetProductModel>
{
    public async Task<GetProductModel> Handle(FindProductQuery query, CancellationToken _) =>
        await repositories.Products
            .FindAsync(query.Id)
            .EnsureIsNotNull(query.Id)
            .MapAsync(GetProductModel.Create);
}