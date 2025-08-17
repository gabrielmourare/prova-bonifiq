using ProvaPub.DTOs;
using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<PagedResult<T>> List(int page, int pageSize);
    }
}
