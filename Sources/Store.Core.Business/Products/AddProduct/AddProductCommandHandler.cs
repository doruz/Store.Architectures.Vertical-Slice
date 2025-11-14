using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Core.Business.Products;

internal sealed class AddProductCommandHandler(RepositoriesContext repositories)
    : IRequestHandler<AddProductCommand, IdModel>
{
    public async Task<IdModel> Handle(AddProductCommand command, CancellationToken _)
    {
        var newProduct = CreateProduct(command);

        await repositories.Products.AddAsync(newProduct);

        return new IdModel(newProduct.Id);
    }

    private static Product CreateProduct(AddProductCommand command) => new
    (
        command.Name,
        command.Price,
        command.Stock
    );
}