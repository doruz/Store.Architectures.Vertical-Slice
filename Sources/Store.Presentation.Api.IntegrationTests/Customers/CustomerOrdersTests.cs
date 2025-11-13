using Store.Core.Business.Orders;

namespace Store.Presentation.Api.IntegrationTests.Customers;

public class CustomerOrdersTests(ApiApplicationFactory factory) : ApiBaseTests(factory)
{
    [Fact]
    public async Task When_CustomerDoesNotHaveOrders_Should_ReturnSuccessWithEmptyDetails()
    {
        // Arrange
        await Database.DeleteCustomerOrders(CurrentCustomer.Id);

        // Act
        var response = await Api.Customer.Orders.GetAllAsync();

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContainContentAsync<OrderSummaryTestModel[]>([]);
    }

    [Fact]
    public async Task When_CustomerHasOrders_Should_ReturnSuccessWithSummariesDetails()
    {
        // Arrange
        await Database.DeleteCustomerOrders(CurrentCustomer.Id);

        var orders = new []
        {
            await SaveNewOrder(cart => cart.Apples(1).Bananas(2)),
            await SaveNewOrder(cart => cart.Apples(3).Bananas(1))
        };

        var expectedOrderSummaries = new []
        {
            new OrderSummaryTestModel
            {
                Id = orders[1].Id,
                OrderedAt = new DateTimeTestModel(orders[1].OrderedAt.Value),
                TotalProducts = 4,
                TotalPrice = new PriceTestModel(3.72m)
            },
            new OrderSummaryTestModel
            {
                Id = orders[0].Id,
                OrderedAt = new DateTimeTestModel(orders[0].OrderedAt.Value),
                TotalProducts = 3,
                TotalPrice = new PriceTestModel(2.49m)
            }
        };

        // Act
        var response = await Api.Customer.Orders.GetAllAsync();

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContainContentAsync(expectedOrderSummaries);
    }

    [Fact]
    public async Task When_OrderDoesNotExists_Should_ReturnNotFound()
    {
        // Act
        var response = await Api.Customer.Orders.FindAsync("unknown_id");

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.NotFound)
            .And.ContainContentAsync(new BusinessErrorTestModel("not_found")); ;
    }

    [Fact]
    public async Task When_OrderExists_Should_ReturnOrderDetails()
    {
        // Arrange
        var order = await SaveNewOrder(cart => cart.Apples(2).Bananas(3));

        var expectedOrderDetails = new OrderDetailsTestModel
        {
            Id = order.Id,
            OrderedAt = new DateTimeTestModel(order.OrderedAt.Value),
            TotalProducts = 5,
            TotalPrice = new PriceTestModel(4.23m),

            Lines = 
            [
                new OrderDetailsLineTestModel
                {
                    ProductId = TestProducts.Apples.Id,
                    ProductName = TestProducts.Apples.Name,
                    ProductPrice = new PriceTestModel(0.99m),

                    Quantity = 2,
                    TotalPrice = new PriceTestModel(1.98m)
                },

                new OrderDetailsLineTestModel
                {
                    ProductId = TestProducts.Bananas.Id,
                    ProductName = TestProducts.Bananas.Name,
                    ProductPrice = new PriceTestModel(0.75m),

                    Quantity = 3,
                    TotalPrice = new PriceTestModel(2.25m)
                }
            ]
        };

        // Act
        var response = await Api.Customer.Orders.FindAsync(order.Id);

        // Assert
        await response.Should()
            .HaveStatusCode(HttpStatusCode.OK)
            .And.ContainContentAsync(expectedOrderDetails);
    }

    private async Task<OrderSummaryModel> SaveNewOrder(Action<UpdateShoppingCartTestModel> shoppingCartActions)
    {
        await Api.Customer.Cart
            .ClearAsync()
            .EnsureIsSuccess();

        await Api.Customer.Cart
            .UpdateAsync(shoppingCartActions)
            .EnsureIsSuccess();

        return await Api.Customer.Cart
            .CheckoutAsync()
            .EnsureIsSuccess()
            .ContentAsAsync<OrderSummaryModel>();
    }
}