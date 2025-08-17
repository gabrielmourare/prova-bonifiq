using Microsoft.AspNetCore.Mvc;
using ProvaPub.API.DTOs;
using ProvaPub.Application.Interfaces;
using ProvaPub.Domain.Models;


namespace ProvaPub.API.Controllers
{
	
	[ApiController]
	[Route("[controller]")]
	public class Parte2Controller :  ControllerBase
	{
		/// <summary>
		/// Precisamos fazer algumas alterações:
		/// 1 - Não importa qual page é informada, sempre são retornados os mesmos resultados. Faça a correção.
		/// 2 - Altere os códigos abaixo para evitar o uso de "new", como em "new ProductService()". Utilize a Injeção de Dependência para resolver esse problema
		/// 3 - Dê uma olhada nos arquivos /Models/CustomerList e /Models/ProductList. Veja que há uma estrutura que se repete. 
		/// Como você faria pra criar uma estrutura melhor, com menos repetição de código? E quanto ao CustomerService/ProductService. Você acha que seria possível evitar a repetição de código?
		/// 
		/// </summary>
	
		IProductService _productService;
		ICustomerService _customerService;
		public Parte2Controller(IProductService productService,ICustomerService customerService)
		{
			_productService = productService;
			_customerService = customerService;
		}
	
		[HttpGet("products")]
        public async Task<ActionResult<ApiResponse<PagedResult<Product>>>> ListProducts(int page = 1, int pageSize = 10)
        {
            var (success, message, data) = await _productService.List(page, pageSize);

            if (!success || data == null)
            {
                
                return BadRequest(new ApiResponse<PagedResult<Product>>(data, message, false, StatusCodes.Status400BadRequest));
            }

            
            return Ok(new ApiResponse<PagedResult<Product>>(data, message, true, 200));
        }
        [HttpGet("customers")]
        public async Task<ActionResult<ApiResponse<PagedResult<Customer>>>> ListCustomers(int page = 1, int pageSize = 10)
        {
            var (success, message, data) = await _customerService.List(page, pageSize);

            if (!success || data == null)
            {
                return BadRequest(new ApiResponse<PagedResult<Customer>>(data, message, false, StatusCodes.Status400BadRequest));
            }

            return Ok(new ApiResponse<PagedResult<Customer>>(data, message, true, StatusCodes.Status200OK));
        }

    }
}
