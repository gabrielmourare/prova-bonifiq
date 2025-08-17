using ProvaPub.API.DTOs;


namespace ProvaPub.Application.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<(bool Success, string Message, PagedResult<T> Response)> List(int page, int pageSize);
        Task<(bool Success, string Message, T Response)> GetById(int id);
    }
}
