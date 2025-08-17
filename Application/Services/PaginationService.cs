using Microsoft.EntityFrameworkCore;
using ProvaPub.API.DTOs;
using ProvaPub.Application.Interfaces;

namespace ProvaPub.Application.Services
{
    public class PaginationService : IPaginationService
    {
        public async Task<PagedResult<T>> PaginateAsync<T>(IQueryable<T> query, int pageNumber, int pageSize) where T : class
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            int totalCount = await query.CountAsync();
            bool hasNext = pageNumber * pageSize < totalCount;

            List<T> items = await query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                HasNext = hasNext
            };
        }
    }
}
