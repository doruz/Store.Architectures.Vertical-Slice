using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;

namespace Store.Infrastructure.Persistence.Cosmos;

internal static class CosmosClientFactory
{
    public static CosmosClient Create(IConfiguration configuration) =>
        new CosmosClientBuilder(configuration["CosmosOptions:ConnectionString"])
            .WithApplicationRegion(configuration["CosmosOptions:ApplicationRegion"])
            .WithContentResponseOnWrite(false)
            .WithSerializerOptions(new CosmosSerializationOptions
            {
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
                IgnoreNullValues = false
            })
            .BuildAndInitializeAsync([])
            .Result;
}