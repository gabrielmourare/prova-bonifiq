using ProvaPub.API.DTOs.Requests;
using ProvaPub.API.DTOs.Responses;
using ProvaPub.Application.Interfaces;
using ProvaPub.Data.Repository;
using ProvaPub.Domain.Models;


namespace ProvaPub.Application.Services
{
    public class OrderService : IOrderService
    {
        TestDbContext _ctx;
        IPaymentService _paymentService;
        ICustomerService _customerService;

        public OrderService(TestDbContext ctx, IPaymentService paymentService, ICustomerService customerService)
        {
            _ctx = ctx;
            _paymentService = paymentService;
            _customerService = customerService;
        }

        public async Task<(bool Success, string Message, OrderResponseDTO response)> PlaceOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            OrderResponseDTO response = new OrderResponseDTO();
            PaymentRequestDTO paymentRequest = new PaymentRequestDTO();

            paymentRequest.PaymentMethod = paymentMethod;
            paymentRequest.PaymentValue = paymentValue;

            var (successCanPurchase, canPurchaseMessage, canPurchaseResponse) = await _customerService.CanPurchase(customerId, paymentValue);

            if (!successCanPurchase)
            {
                return (successCanPurchase, canPurchaseMessage, response);
            }

            var (success, message, data) = await _paymentService.ProcessPayment(paymentRequest);

            if (!success)
            {
                response.PaymentValue = paymentValue;
                response.CustomerId = customerId;
                return (false, message, response);
            }


            var (successCustomer, customerMessage, customer) = await _customerService.GetById(customerId);

            if (!successCustomer)
            {
                response.PaymentValue = paymentValue;
                response.CustomerId = customerId;
                return (false, customerMessage, response);
            }


            Order newOrder = new Order()
            {
                CustomerId = customer.Id,
                OrderDate = DateTime.Now.ToUniversalTime(),
                Value = paymentValue
            };

            Order order = await InsertOrder(newOrder);

            if (order == null)
            {
                return (false, "Erro ao gerar pedido", response);
            }

            var brazilOffset = TimeSpan.FromHours(-3);

            response.PaymentValue = order.Value;
            response.CustomerId = order.CustomerId;
            response.OrderDate = new DateTimeOffset(order.OrderDate, TimeSpan.Zero).ToOffset(brazilOffset).DateTime;


            return (true, message, response);

        }

        public async Task<Order> InsertOrder(Order order)
        {
            //Insere pedido no banco de dados
            return (await _ctx.Orders.AddAsync(order)).Entity;
        }

    }
}
