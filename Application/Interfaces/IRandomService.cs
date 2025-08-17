using ProvaPub.API.DTOs.Responses;

namespace ProvaPub.Application.Interfaces
{
    public interface IRandomService
    {
        Task<(bool Success, string Message, RandomResponseDTO Response)> GetRandom();
    }
}
