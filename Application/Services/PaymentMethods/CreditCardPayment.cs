using ProvaPub.Application.Interfaces;

namespace ProvaPub.Application.Services.PaymentMethods
{
    public class CreditCardPayment : IPaymentStrategy
    {
        public string Type => "CreditCard";

        bool IPaymentStrategy.Pay(decimal amount)
        {
            Random random = new Random();
            return random.Next(2) == 1;
        }
    }
}
