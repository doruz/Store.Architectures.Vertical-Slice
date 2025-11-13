using Store.Core.Shared;
using System.ComponentModel.DataAnnotations;

namespace Store.Core.Business.Products;

public record EditProductModel
{
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength)]
    public string? Name { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = ValidationMessages.MinValue)]
    public decimal? Price { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = ValidationMessages.MinValue)]
    public int? Stock { get; init; }
}