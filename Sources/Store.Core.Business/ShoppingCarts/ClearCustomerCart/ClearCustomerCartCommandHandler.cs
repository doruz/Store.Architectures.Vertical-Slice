using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.ShoppingCarts;

internal sealed class ClearCustomerCartCommandHandler(RepositoriesContext repositories, ICurrentCustomer currentCustomer)
    : IRequestHandler<ClearCustomerCartCommand>
{
    public Task Handle(ClearCustomerCartCommand request, CancellationToken _) 
        => repositories.ShoppingCarts.DeleteAsync(currentCustomer.Id);
}