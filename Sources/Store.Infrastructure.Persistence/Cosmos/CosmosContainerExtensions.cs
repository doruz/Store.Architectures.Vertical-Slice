using System.Net;
using EnsureThat;
using Microsoft.Azure.Cosmos;
using Store.Core.Domain.Entities;

namespace Store.Infrastructure.Persistence.Cosmos;

internal static class CosmosContainerExtensions
{
    public static async Task<T?> FindAsync<T>(this Container container, string id, PartitionKey partitionKey)
        where T : BaseEntity
    {
        EnsureArg.IsNotNullOrEmpty(id, nameof(id));
        EnsureArg.IsNotDefault(partitionKey, nameof(partitionKey));

        try
        {
            return await container.ReadItemAsync<T>(id, partitionKey);
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public static async Task<bool> DeleteAsync<T>(this Container container, string id, PartitionKey partitionKey)
        where T : BaseEntity
    {
        EnsureArg.IsNotNullOrEmpty(id, nameof(id));

        try
        {
            await container.DeleteItemAsync<T>(id, partitionKey);
            return true;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }
    }
}