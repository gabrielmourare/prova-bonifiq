using ProvaPub.Application.Interfaces;

namespace ProvaPub.Application.Services.PaymentMethods
{
    public class PixPayment : IPaymentStrategy
    {
        public string Type => "PIX";

        bool IPaymentStrategy.Pay(decimal amount)
        {
            return true;
        }
    }
}
