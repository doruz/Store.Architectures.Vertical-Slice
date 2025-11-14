using System.Reflection;
using System.Text.Json;
using Store.Core.Business;
using Store.Core.Shared;
using Store.Infrastructure.Persistence;

public static class ApiLayer
{
    public static Assembly Assembly => typeof(ApiLayer).Assembly;

    public static IServiceCollection AddCurrentSolution(this IServiceCollection services, IConfiguration configuration)
    {
        return services

            .AddBusiness()
            .AddPersistence(configuration)

            .AddTransient<ICurrentCustomer, CurrentCustomer>()
            .AddHostedService<AppInitializationService>();
    }

    public static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        return services.AddOpenApi(options =>
        {
            options.AddOperationTransformer((operation, _, _) =>
            {
                foreach (var parameter in operation.Parameters ?? [])
                {
                    parameter.Name = JsonNamingPolicy.CamelCase.ConvertName(parameter.Name);
                }

                return Task.CompletedTask;
            });
        });
    }
}