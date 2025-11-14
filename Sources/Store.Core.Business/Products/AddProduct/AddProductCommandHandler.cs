using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Core.Business.Products;

internal sealed class AddProductCommandHandler(RepositoriesContext repositories)
    : IRequestHandler<AddProductCommand, ProductModel>
{
    public async Task<ProductModel> Handle(AddProductCommand command, CancellationToken _)
    {
        var newProduct = CreateProduct(command);

        await repositories.Products.AddAsync(newProduct);

        return ProductModel.Create(newProduct);
    }

    private static Product CreateProduct(AddProductCommand command) => new
    (
        command.Name,
        command.Price,
        command.Stock
    );
}