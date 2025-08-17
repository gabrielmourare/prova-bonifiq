using ProvaPub.Application.Interfaces;

namespace ProvaPub.Application.Factories
{
    public interface IPaymentFactory
    {
        IPaymentStrategy GetStrategy(string type);
    }
}
