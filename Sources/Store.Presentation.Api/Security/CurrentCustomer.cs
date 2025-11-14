using Store.Core.Shared;

internal sealed class CurrentCustomer : ICurrentCustomer
{
    // TODO: replace with real authentication mechanism.
    public string Id => Guid.Empty.ToString();
}