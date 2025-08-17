namespace ProvaPub.Application.Interfaces
{
    public interface IPaymentStrategy
    {
        string Type { get; }
        bool Pay(decimal amount);
    }
}
