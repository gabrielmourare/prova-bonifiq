namespace ProvaPub.API.DTOs.Responses
{
    public class OrderResponseDTO
    {
        public decimal PaymentValue { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
      
    }
}
