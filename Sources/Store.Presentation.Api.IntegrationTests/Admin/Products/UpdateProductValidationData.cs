namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

internal sealed class UpdateProductValidationData : ValidationTheoryData<object>
{
    protected override object ValidModel { get; } = new();

    public UpdateProductValidationData()
    {
        Add(
            new { Name = Random.Strings.Generate(101) },
            "Name", 
            "Value must have maximum length 100."
        );

        Add(
            new { Price = Random.Numbers.Generate(int.MinValue, 0) },
            "Price", 
            "Value must be greater or equal than 0."
        );

        Add(
            new { Stock = Random.Numbers.Generate(int.MinValue, 0) },
            "Stock",
            "Value must be greater or equal than 0."
        );
    }
}