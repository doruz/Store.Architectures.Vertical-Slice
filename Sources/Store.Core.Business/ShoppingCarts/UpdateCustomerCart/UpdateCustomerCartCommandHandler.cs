using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.ShoppingCarts;

internal sealed class UpdateCustomerCartCommandHandler(RepositoriesContext repositories, ICurrentCustomer currentCustomer)
    : IRequestHandler<UpdateCustomerCartCommand>
{
    public async Task Handle(UpdateCustomerCartCommand request, CancellationToken _)
    {
        if (request.Lines.IsEmpty())
        {
            return;
        }

        var shoppingCart = await repositories.ShoppingCarts.FindOrEmptyAsync(currentCustomer.Id);

        shoppingCart.UpdateOrRemoveLines(await GetValidLines(request.Lines));

        await repositories.ShoppingCarts.AddOrUpdateAsync(shoppingCart);
    }

    private async Task<ShoppingCartLine[]> GetValidLines(IEnumerable<UpdateCustomerCartLineModel> cartLines)
    {
        var lines = await cartLines
            .Select(async cartLine => new
            {
                CartLine = cartLine,
                Product = await repositories.Products.FindAsync(cartLine.ProductId)
            })
            .ToListAsync();

        lines.ForEach(l =>
        {
            l.Product
                .EnsureExists(l.CartLine.ProductId)
                .EnsureStockIsAvailable(l.CartLine.Quantity);
        });

        return lines
            .Select(l => ToShoppingCartLine(l.CartLine))
            .ToArray();
    }

    private static ShoppingCartLine ToShoppingCartLine(UpdateCustomerCartLineModel cartLine)
        => new(cartLine.ProductId, cartLine.Quantity);
}