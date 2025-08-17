using ProvaPub.Application.Interfaces;


namespace ProvaPub.Application.Factories
{
    public class PaymentFactory : IPaymentFactory
    {
        private readonly IEnumerable<IPaymentStrategy> _strategies;
        public PaymentFactory(IEnumerable<IPaymentStrategy> strategies)
        {
            _strategies = strategies;
        }

        public IPaymentStrategy GetStrategy(string type)
        {
            var strategy = _strategies.FirstOrDefault(s =>
                s.Type.Equals(type, StringComparison.OrdinalIgnoreCase));

            if (strategy == null)
                throw new ArgumentException($"Tipo de pagamento inválido: {type}");

            return strategy;
        }
    }
}
