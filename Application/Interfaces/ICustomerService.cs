using ProvaPub.API.DTOs.Responses;
using ProvaPub.Domain.Models;

namespace ProvaPub.Application.Interfaces
{
    public interface ICustomerService : IBaseService<Customer>
    {
        Task<(bool Success, string Message, CustomerResponseDTO Response)> CanPurchase(int customerId, decimal purchaseValue);
        
    }
}
