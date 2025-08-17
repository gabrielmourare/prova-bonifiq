using ProvaPub.Application.Interfaces;

namespace ProvaPub.Application.Services.PaymentMethods
{
    public class PayPalPayment : IPaymentStrategy
    {
        public string Type => "PayPal";
              

        bool IPaymentStrategy.Pay(decimal amount)
        {
            return true;
        }
    }
}
