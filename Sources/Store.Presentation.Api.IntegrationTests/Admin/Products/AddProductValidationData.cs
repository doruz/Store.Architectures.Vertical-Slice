namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

internal sealed class AddProductValidationData : ValidationTheoryData<NewProductTestModel>
{
    protected override NewProductTestModel ValidModel { get; } = NewProductTestModel.CreateRandom();

    public AddProductValidationData()
    {
        Add(ValidModel with { Name = "" }, "Name", "Field is required.");
        Add(ValidModel with { Name = Random.Strings.Generate(101) }, "Name", "Value must have maximum length 100.");
        Add(ValidModel with { Price = Random.Numbers.Generate(int.MinValue, 0) }, "Price", "Value must be greater or equal than 0.");
        Add(ValidModel with { Stock = Random.Numbers.Generate(int.MinValue, 0) }, "Stock", "Value must be greater or equal than 0.");
    }
}