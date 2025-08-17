using ProvaPub.DTOs;

namespace ProvaPub.Interfaces
{
    public interface IPaginationService
    {
        Task<PagedResult<T>> PaginateAsync<T>(IQueryable<T> query, int pageNumber, int pageSize) where T : class;

    }
}
