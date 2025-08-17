using ProvaPub.API.DTOs.Requests;
using ProvaPub.API.DTOs.Responses;

namespace ProvaPub.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<(bool Success, string Message, PaymentResponseDTO response)> ProcessPayment(PaymentRequestDTO paymentRequest);
    }
}
