using Store.Core.Domain.Entities;

namespace Store.Infrastructure.Persistence.InMemory
{
    internal sealed class InMemoryDatabase
    {
        public List<Product> Products { get; } = [];

        public List<ShoppingCart> ShoppingCarts { get; } = [];

        public List<Order> Orders { get; } = [];
    }
}
