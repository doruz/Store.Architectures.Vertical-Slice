using EnsureThat;
using Store.Core.Domain.Entities;

namespace Store.Core.Business.ShoppingCarts;

internal static class ShoppingCartsMapper
{
    //public static ShoppingCartLineModel ToShoppingCartLineModel(this ShoppingCartLine cartLine, Product product)
    //{
    //    EnsureArg.IsNotNull(cartLine, nameof(cartLine));
    //    EnsureArg.IsNotNull(product, nameof(product));

    //    return new ShoppingCartLineModel
    //    {
    //        ProductId = product.Id,
    //        ProductName = product.Name,
    //        ProductPrice = PriceModel.Create(product.Price),
    //        TotalPrice = PriceModel.Create(product.Price * cartLine.Quantity),
    //        Quantity = cartLine.Quantity,
    //    };
    //}

    public static ShoppingCartLine ToShoppingCartLine(this EditShoppingCartLineModel model)
        => new (model.ProductId, model.Quantity);
}