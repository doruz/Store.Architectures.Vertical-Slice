global using Store.Core.Business.Shared;
global using MediatR;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Business.ShoppingCarts;

namespace Store.Core.Business;

public static class BusinessLayer
{
    public static Assembly Assembly => typeof(BusinessLayer).Assembly;

    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        return services
            .AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly))

            .AddScoped<ShoppingCartCheckoutService>();
    }
}