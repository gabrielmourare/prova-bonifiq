using Azure;
using Microsoft.AspNetCore.Mvc;
using ProvaPub.API.DTOs.Responses;
using ProvaPub.Application.Interfaces;
using ProvaPub.API.DTOs;
using ProvaPub.Application.Services;

namespace ProvaPub.API.Controllers
{
    /// <summary>
    /// Ao rodar o código abaixo o serviço deveria sempre retornar um número diferente, mas ele fica retornando sempre o mesmo número.
    /// 1 - Faça as alterações para que o retorno seja sempre diferente
    /// 2 - Tome cuidado 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Parte1Controller : ControllerBase
    {
        private readonly IRandomService _randomService;

        public Parte1Controller(IRandomService randomService)
        {
            _randomService = randomService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRandomNumber()
        {
            var (success, message, data) = await _randomService.GetRandom();

            if (!success)
            {
                return BadRequest(new ApiResponse<RandomResponseDTO>(data, message, false, StatusCodes.Status400BadRequest));
            }

            return Ok(new ApiResponse<RandomResponseDTO>(data, message, false, StatusCodes.Status200OK));

        }
    }
}
