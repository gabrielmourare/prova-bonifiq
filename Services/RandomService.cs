using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class RandomService : IRandomService
	{
		int seed;
        TestDbContext _ctx;
        public readonly Random _random;
		public RandomService()
        {
            var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Teste;Trusted_Connection=True;")
    .Options;
            seed = Guid.NewGuid().GetHashCode();

            _ctx = new TestDbContext(contextOptions);
            _random = new Random();
        }
        public async Task<int> GetRandom()
		{
            var allNumbers = Enumerable.Range(0, 100);
           

            List<int> usedNumbers = await _ctx.Numbers
                               .Select(n => n.Number)
                               .ToListAsync();

            List<int> availableNumbers = allNumbers.Except(usedNumbers).ToList();

            if (!availableNumbers.Any())
            {
                throw new InvalidOperationException("Todos os números já foram usados.");
            }

            Random random = new Random();
            int number = availableNumbers[random.Next(availableNumbers.Count)];

            _ctx.Numbers.Add(new RandomNumber { Number = number });
            await _ctx.SaveChangesAsync();

            return number;
        }

	}
}
