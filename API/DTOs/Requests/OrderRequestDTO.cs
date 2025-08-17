namespace ProvaPub.API.DTOs.Requests
{
    public class OrderRequestDTO
    {
        /// <summary>
        /// Método de pagamento
        /// </summary>
        public string PaymentMethod { get; set; }
        /// <summary>
        /// Valor do pagamento
        /// </summary>
        public decimal PaymentValue { get; set; }
        /// <summary>
        /// ID do cliente
        /// </summary>
        public int CustomerID { get; set; }
    }
}
