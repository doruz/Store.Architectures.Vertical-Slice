namespace Store.Presentation.Api.IntegrationTests;

public record UpdateShoppingCartLineTestModel(string ProductId, int Quantity);


public sealed class UpdateShoppingCartTestModel
{
    public List<UpdateShoppingCartLineTestModel> Lines { get; } = [];

    public UpdateShoppingCartTestModel Apples(int quantity)
        => With(TestProducts.Apples.Id, quantity);

    public UpdateShoppingCartTestModel Bananas(int quantity)
        => With(TestProducts.Bananas.Id, quantity);

    public UpdateShoppingCartTestModel With(string productId, int quantity)
    {
        Lines.Add(new UpdateShoppingCartLineTestModel(productId, quantity));
        return this;
    }
}