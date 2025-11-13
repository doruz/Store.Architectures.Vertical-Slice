using Store.Core.Business.Products;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.ShoppingCarts;

public sealed class ShoppingCartsService(RepositoriesContext repositories, ICurrentCustomer currentCustomer)
{
    public async Task<ShoppingCartModel> GetCurrentCustomerCart()
    {
        var shoppingCart = await repositories.ShoppingCarts.FindOrEmptyAsync(currentCustomer.Id);

        var cartLines = await shoppingCart.Lines
            .Select(async cartLine => new
            {
                CartLine = cartLine,
                Product = await repositories.Products.FindAsync(cartLine.ProductId)
            })
            .ToListAsync();

        var lines = cartLines
            .Where(l => l.Product != null)
            .Select(l => l.CartLine.ToShoppingCartLineModel(l.Product!));

        var totalCartPrice = cartLines
            .Where(l => l.Product != null)
            .Select(l => l.Product!.Price * l.CartLine.Quantity)
            .Sum();

        return new ShoppingCartModel
        {
            Lines = lines,
            TotalPrice = PriceModel.Create(totalCartPrice)
        };
    }

    public Task ClearCurrentCustomerCart()
        => repositories.ShoppingCarts.DeleteAsync(currentCustomer.Id);

    public async Task UpdateCurrentCustomerCart(params EditShoppingCartLineModel[] lines)
    {
        if (lines.IsEmpty())
        {
            return;
        }

        var shoppingCart = await repositories.ShoppingCarts.FindOrEmptyAsync(currentCustomer.Id);

        shoppingCart.UpdateOrRemoveLines(await GetValidLines(lines));
       
        await repositories.ShoppingCarts.AddOrUpdateAsync(shoppingCart);
    }

    private async Task<ShoppingCartLine[]> GetValidLines(IEnumerable<EditShoppingCartLineModel> cartLines)
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
                .EnsureIsNotNull(l.CartLine.ProductId)
                .EnsureStockIsAvailable(l.CartLine.Quantity);
        });

        return lines
            .Select(l => l.CartLine.ToShoppingCartLine())
            .ToArray();
    }
}