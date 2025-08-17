using ProvaPub.DTOs;
using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface ICustomerService : IBaseService<Customer>
    {
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}
