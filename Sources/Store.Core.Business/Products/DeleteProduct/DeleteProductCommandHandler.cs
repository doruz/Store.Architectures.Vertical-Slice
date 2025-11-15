using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Core.Business.Products;

internal sealed class DeleteProductCommandHandler(RepositoriesContext repositories)
    : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand command, CancellationToken _)
    {
        var product = await repositories.Products
            .FindAsync(command.Id)
            .EnsureExists(command.Id);

        product.MarkAsDeleted();

        await repositories.Products.UpdateAsync(product);
    }
}