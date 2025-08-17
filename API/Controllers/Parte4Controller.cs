using Microsoft.AspNetCore.Mvc;
using ProvaPub.API.DTOs.Responses;
using ProvaPub.Application.Interfaces;
using ProvaPub.API.DTOs;


namespace ProvaPub.API.Controllers
{

    /// <summary>
    /// O Código abaixo faz uma chmada para a regra de negócio que valida se um consumidor pode fazer uma compra.
    /// Crie o teste unitário para esse Service. Se necessário, faça as alterações no código para que seja possível realizar os testes.
    /// Tente criar a maior cobertura possível nos testes.
    /// 
    /// Utilize o framework de testes que desejar. 
    /// Crie o teste na pasta "Tests" da solution
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Parte4Controller : ControllerBase
    {
        ICustomerService _customerService;

        public Parte4Controller(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("CanPurchase")]
        public async Task<IActionResult> CanPurchase(int customerId, decimal purchaseValue)
        {
            var (success, message, data) = await _customerService.CanPurchase(customerId, purchaseValue);

            if(!success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<CustomerResponseDTO>(data, message, success, StatusCodes.Status400BadRequest));
            }

            return StatusCode(StatusCodes.Status200OK, new ApiResponse<CustomerResponseDTO>(data, message, success, StatusCodes.Status200OK));
        }
    }
}
