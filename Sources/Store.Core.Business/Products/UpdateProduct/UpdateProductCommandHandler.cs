using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Core.Business.Products;

internal sealed class UpdateProductCommandHandler(RepositoriesContext repositories)
    : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand command, CancellationToken _)
    {
        var existingProduct = await repositories.Products
            .FindAsync(command.Id)
            .EnsureExists(command.Id);

        existingProduct.Update(command.Name, command.Price, command.Stock);

        await repositories.Products.UpdateAsync(existingProduct);
    }
}