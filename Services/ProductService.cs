using ProvaPub.DTOs;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using System.Threading.Tasks;

namespace ProvaPub.Services
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

        public async Task<PagedResult<Product>> List(int page = 1, int pageSize = 10)
        {
            var query = _ctx.Products.OrderBy(p => p.Id);

            return await _paginationService.PaginateAsync(query, page, pageSize);

        }


    }
}
