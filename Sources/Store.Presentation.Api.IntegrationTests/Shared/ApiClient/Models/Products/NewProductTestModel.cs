namespace Store.Presentation.Api.IntegrationTests;

public record NewProductTestModel(string Name, decimal Price, int Stock)
{
    internal static NewProductTestModel CreateRandom() => new
    (
        Name: Random.Strings.Generate(1, 100),
        Price: Random.Numbers.Generate(0, int.MaxValue),
        Stock: Random.Numbers.Generate(0, int.MaxValue)
    );

    internal ReadProductTestModel GetExpectedDetails(string id) => new()
    {
        Id = id,
        Name = Name,
        Price = new PriceTestModel(Price),
        Stock = Stock
    };
}