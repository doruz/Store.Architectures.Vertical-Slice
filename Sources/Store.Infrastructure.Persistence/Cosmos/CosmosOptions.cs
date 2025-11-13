namespace Store.Infrastructure.Persistence.Cosmos;

internal sealed record CosmosOptions
{
    public required string DatabaseName { get; init; }
    public required int MaxThroughput { get; init; }
}