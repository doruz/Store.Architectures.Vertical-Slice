using Store.Core.Business.Orders;
using Store.Core.Business.Products;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.ShoppingCarts;

public sealed class ShoppingCartCheckoutService(RepositoriesContext repositories, ICurrentCustomer currentCustomer)
{
    public async Task<OrderSummaryModel> CheckoutCurrentCustomerCart()
    {
        var shoppingCartItems = await GetShoppingCartItems();

        shoppingCartItems.ForEach(l => l.Product.EnsureStockIsAvailable(l.CartLine.Quantity));

        var orderLines = shoppingCartItems
            .Select(item => OrderLine.Create(item.CartLine, item.Product))
            .ToList();

        var customerOrder = new Order(currentCustomer.Id, orderLines);
        await repositories.Orders.SaveOrderAsync(customerOrder);
        await repositories.ShoppingCarts.DeleteAsync(currentCustomer.Id);

        await UpdateProductsStock(shoppingCartItems);
        
        // TODO: return just the order id
        return customerOrder.ToOrderSummaryModel();
    }

    private async Task<List<(ShoppingCartLine CartLine, Product Product)>> GetShoppingCartItems()
    {
        var shoppingCart = await repositories.ShoppingCarts.FindOrEmptyAsync(currentCustomer.Id);

        shoppingCart.EnsureIsNotEmpty();

        return await shoppingCart.Lines
            .Where(cartLine => cartLine.Quantity > 0)
            .Select(async cartLine =>
            (
                cartLine,
                (await repositories.Products.FindAsync(cartLine.ProductId))!
            ))
            .ToListAsync();
    }

    private async Task UpdateProductsStock(List<(ShoppingCartLine CartLine, Product Product)> items)
    {
        foreach (var item in items)
        {
            item.Product.DecreaseStock(item.CartLine.Quantity);
            await repositories.Products.UpdateAsync(item.Product);
        }
    }
}