namespace ProvaPub.API.DTOs.Requests
{
    public class PaymentRequestDTO
    {
        public string PaymentMethod { get; set; }
        public decimal PaymentValue { get; set; }
    }
}
