using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.ShoppingCarts;

internal sealed class GetCustomerCartQueryHandler(RepositoriesContext repositories, ICurrentCustomer currentCustomer)
    : IRequestHandler<GetCustomerCartQuery, GetCustomerCartQueryResult>
{
    public async Task<GetCustomerCartQueryResult> Handle(GetCustomerCartQuery request, CancellationToken _)
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
            .Select(l => ToShoppingCartLineModel(l.CartLine, l.Product!));

        var totalCartPrice = cartLines
            .Where(l => l.Product != null)
            .Select(l => l.Product!.Price * l.CartLine.Quantity)
            .Sum();

        return new GetCustomerCartQueryResult
        {
            Lines = lines,
            TotalPrice = PriceModel.Create(totalCartPrice)
        };
    }

    private static GetCustomerCartLineModel ToShoppingCartLineModel(ShoppingCartLine cartLine, Product product)
    {
        EnsureArg.IsNotNull(cartLine, nameof(cartLine));
        EnsureArg.IsNotNull(product, nameof(product));

        return new GetCustomerCartLineModel
        {
            ProductId = product.Id,
            ProductName = product.Name,
            ProductPrice = PriceModel.Create(product.Price),
            TotalPrice = PriceModel.Create(product.Price * cartLine.Quantity),
            Quantity = cartLine.Quantity,
        };
    }
}