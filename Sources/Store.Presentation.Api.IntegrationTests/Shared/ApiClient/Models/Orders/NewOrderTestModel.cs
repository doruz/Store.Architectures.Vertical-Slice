namespace Store.Presentation.Api.IntegrationTests;

public record NewOrderTestModel
{
    public required string Id { get; init; }

    public required DateTimeTestModel OrderedAt { get; init; }
}