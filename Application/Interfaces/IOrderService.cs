using ProvaPub.API.DTOs.Responses;


namespace ProvaPub.Application.Interfaces
{
    public interface IOrderService
    {
        Task<(bool Success, string Message, OrderResponseDTO response)> PlaceOrder(string paymentMethod, decimal paymentValue, int customerId);

    }
}
