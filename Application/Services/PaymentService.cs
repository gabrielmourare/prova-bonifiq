using ProvaPub.API.DTOs.Requests;
using ProvaPub.API.DTOs.Responses;
using ProvaPub.Application.Factories;
using ProvaPub.Application.Interfaces;

namespace ProvaPub.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentFactory _paymentFactory;

        public PaymentService(IPaymentFactory paymentFactory)
        {
            _paymentFactory = paymentFactory;
        }

        public async Task<(bool Success, string Message, PaymentResponseDTO response)> ProcessPayment(PaymentRequestDTO paymentRequest)
        {
            PaymentResponseDTO response = new PaymentResponseDTO();

            var strategy = _paymentFactory.GetStrategy(paymentRequest.PaymentMethod);
            if (!strategy.Pay(paymentRequest.PaymentValue))
            {
                return (false, "Pagamento não realizado", response);
            }

            response.PaymentMethod = paymentRequest.PaymentMethod;
            response.PaymentValue = paymentRequest.PaymentValue;           

            return (true, "Pagamento efetuado com sucesso!", response);
        }
    }
}
