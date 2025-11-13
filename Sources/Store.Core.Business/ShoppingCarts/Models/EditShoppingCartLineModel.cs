using Store.Core.Shared;
using System.ComponentModel.DataAnnotations;

namespace Store.Core.Business.ShoppingCarts;

public record EditShoppingCartLineModel
{
    [Required(ErrorMessage = ValidationMessages.Required)]
    public required string ProductId { get; init; }

    [Range(0, 10, ErrorMessage = ValidationMessages.Range)]
    public required int Quantity { get; init; }
}