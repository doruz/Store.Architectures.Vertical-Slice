using System.ComponentModel.DataAnnotations;
using Store.Core.Shared;

namespace Store.Core.Business.Products;

public record AddProductCommand : IRequest<ProductModel>
{
    [Required(ErrorMessage = ValidationMessages.Required)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength)]
    public required string Name { get; init; }

    [Required(ErrorMessage = ValidationMessages.Required)]
    [Range(0, int.MaxValue, ErrorMessage = ValidationMessages.MinValue)]
    public decimal Price { get; init; }

    [Required(ErrorMessage = ValidationMessages.Required)]
    [Range(0, int.MaxValue, ErrorMessage = ValidationMessages.MinValue)]
    public int Stock { get; init; }
}