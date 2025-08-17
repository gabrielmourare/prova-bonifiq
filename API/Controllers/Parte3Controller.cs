using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaPub.API.DTOs.Requests;
using ProvaPub.API.DTOs.Responses;
using ProvaPub.Application.Interfaces;
using ProvaPub.API.DTOs;

namespace ProvaPub.API.Controllers
{

    /// <summary>
    /// Esse teste simula um pagamento de uma compra.
    /// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
    /// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
    /// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
    /// 
    /// Outra parte importante é em relação à data (OrderDate) do objeto Order. Ela deve ser salva no banco como UTC mas deve retornar para o cliente no fuso horário do Brasil. 
    /// Demonstre como você faria isso.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Parte3Controller : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;


        public Parte3Controller(IOrderService orderService, IPaymentService paymentService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
        }
        [HttpPost("orders")]
        public async Task<IActionResult> PlaceOrder(OrderRequestDTO orderRequest)
        {
            OrderResponseDTO response = new OrderResponseDTO();

            if (orderRequest == null)
            {
                return BadRequest(new ApiResponse<OrderResponseDTO>(response, "Requisição Inválida", false, StatusCodes.Status400BadRequest));
            }

            if(orderRequest.CustomerID == 0)
            {
                return BadRequest(new ApiResponse<OrderResponseDTO>(response, "Cliente/Usuário inválido ", false, StatusCodes.Status400BadRequest));
            }

            if (orderRequest.PaymentValue == 0)
            {
                return BadRequest(new ApiResponse<OrderResponseDTO>(response, "Valor inválido ", false, StatusCodes.Status400BadRequest));
            }

            if (string.IsNullOrWhiteSpace(orderRequest.PaymentMethod))
            {
                return BadRequest(new ApiResponse<OrderResponseDTO>(response, "Método de pagamento inválido", false, StatusCodes.Status400BadRequest));
            }

            var (success, message, data) = await _orderService.PlaceOrder(orderRequest.PaymentMethod, orderRequest.PaymentValue, orderRequest.CustomerID);


            if (!success)
            {
                //Aqui podemos melhorar para especificar o tipo de erro, se necessário.
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<OrderResponseDTO>(data, message, success, StatusCodes.Status400BadRequest));
            }

            response = data;

            return StatusCode(StatusCodes.Status201Created, new ApiResponse<OrderResponseDTO>(response, "Pedido feito com sucesso", true, StatusCodes.Status201Created));


        }
    }
}
