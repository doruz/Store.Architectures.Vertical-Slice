using Microsoft.Azure.Cosmos;

namespace Store.Infrastructure.Persistence.Cosmos;

internal static class CosmosPartitionKeys
{
    public static PartitionKey ToPartitionKey(this string key) => new(key);
}