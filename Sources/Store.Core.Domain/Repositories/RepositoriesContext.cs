namespace Store.Core.Domain.Repositories;

public sealed class RepositoriesContext(
    IProductsRepository products,
    IShoppingCartsRepository shoppingCarts,
    IOrdersRepository orders)
{
    public IProductsRepository Products { get; } = products;

    public IShoppingCartsRepository ShoppingCarts { get; } = shoppingCarts;

    public IOrdersRepository Orders { get; } = orders;
}