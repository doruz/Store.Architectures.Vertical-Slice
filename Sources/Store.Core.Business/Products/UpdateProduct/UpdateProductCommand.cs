using Store.Core.Shared;
using System.ComponentModel.DataAnnotations;
using EnsureThat;

namespace Store.Core.Business.Products;

public sealed record UpdateProductCommand : IRequest
{
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength)]
    public string? Name { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = ValidationMessages.MinValue)]
    public decimal? Price { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = ValidationMessages.MinValue)]
    public int? Stock { get; init; }

    internal string Id { get; init; } = string.Empty;

    public UpdateProductCommand For(string id) => this with
    {
        Id = EnsureArg.IsNotNullOrEmpty(id)
    };
}