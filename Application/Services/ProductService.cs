using ProvaPub.API.DTOs;
using ProvaPub.Application.Interfaces;
using ProvaPub.Data.Repository;
using ProvaPub.Domain.Models;
using System.Threading.Tasks;

namespace ProvaPub.Application.Services
{
    public class ProductService : IProductService
    {
        TestDbContext _ctx;
        IPaginationService _paginationService;
        public ProductService(TestDbContext ctx, IPaginationService paginationService)
        {
            _ctx = ctx;
            _paginationService = paginationService;
        }

        public async Task<(bool Success, string Message, Product Response)> GetById(int id)
        {
            Product product = (Product)_ctx.Products.Where(p => p.Id == id);
            return (true, "Produto encontrado", product);
        }

        public async Task<(bool Success, string Message, PagedResult<Product> Response)> List(int page = 1, int pageSize = 10)
        {

            var query = _ctx.Products.OrderBy(p => p.Id);
            var data = await _paginationService.PaginateAsync(query, page, pageSize);

            if(data == null)
            {
                return (false, "Erro ao paginar dados", data);
            }
            
            return(true, string.Empty, data);


        }


    }
}
