using Microsoft.EntityFrameworkCore;
using ProvaPub.API.DTOs.Responses;
using ProvaPub.Application.Interfaces;
using ProvaPub.Data.Repository;
using ProvaPub.Domain.Models;

namespace ProvaPub.Application.Services
{
	public class RandomService : IRandomService
	{
		int seed;
        TestDbContext _ctx;
        public readonly Random _random;
		public RandomService(TestDbContext ctx)
        {
            _ctx = ctx;
            _random = new Random();
        }
        public async Task<(bool Success, string Message, RandomResponseDTO Response)> GetRandom()
		{
            RandomResponseDTO response = new RandomResponseDTO();

            var allNumbers = Enumerable.Range(0, 100);
           
            List<int> usedNumbers = await _ctx.Numbers
                               .Select(n => n.Number)
                               .ToListAsync();

            List<int> availableNumbers = allNumbers.Except(usedNumbers).ToList();

            if (!availableNumbers.Any())
            {
                return(false,"Todos os números já foram usados.", response);
            }

            Random random = new Random();
            int number = availableNumbers[random.Next(availableNumbers.Count)];

            _ctx.Numbers.Add(new RandomNumber { Number = number });
            await _ctx.SaveChangesAsync();

            response.randomNumber = number;

            return (true, "Número gerado com sucesso", response);
        }

	}
}
