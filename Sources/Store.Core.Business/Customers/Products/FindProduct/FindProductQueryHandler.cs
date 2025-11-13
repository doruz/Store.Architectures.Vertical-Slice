using Store.Core.Business.Products;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.Customers.Products;

internal sealed class FindProductQueryHandler(RepositoriesContext repositories)
    : IRequestHandler<FindProductQuery, ProductModel>
{
    public async Task<ProductModel> Handle(FindProductQuery request, CancellationToken _)
    {
        var product = await repositories.Products.FindAsync(request.Id);

        return product
            .EnsureIsNotNull(request.Id)
            .Map(ToProductModel);
    }

    private static ProductModel ToProductModel(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = PriceModel.Create(product.Price),
        Stock = product.Stock
    };
}