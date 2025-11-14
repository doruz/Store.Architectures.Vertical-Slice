using Store.Core.Domain.Entities;

namespace Store.Core.Business.Shared;

public record PriceModel(decimal Value, string Currency, string Display)
{
    public static PriceModel Create(Price price) 
        => new(price.Value, price.Currency, price.ToString());
}