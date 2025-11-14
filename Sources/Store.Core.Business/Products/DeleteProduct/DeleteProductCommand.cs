namespace Store.Core.Business.Products;

public sealed record DeleteProductCommand(string Id) : IRequest;