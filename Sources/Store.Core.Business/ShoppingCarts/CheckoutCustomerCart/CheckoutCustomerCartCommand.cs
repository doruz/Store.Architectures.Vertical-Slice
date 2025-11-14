using Store.Core.Business.Orders;

namespace Store.Core.Business.ShoppingCarts;

public sealed record CheckoutCustomerCartCommand : IRequest<IdModel>;