using Store.Core.Domain.Entities;

namespace Store.Core.Business.ShoppingCarts;

internal static class ShoppingCartErrors
{
    public static ShoppingCart EnsureIsNotEmpty(this ShoppingCart? shoppingCart)
    {
        if (shoppingCart is null || shoppingCart.IsEmpty())
        {
            throw new BusinessException(BusinessError.NotFound("shopping_cart_is_empty"));
        }

        return shoppingCart;
    } 
}