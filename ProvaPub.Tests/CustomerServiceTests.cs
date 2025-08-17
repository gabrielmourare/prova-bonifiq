namespace ProvaPub.Tests
{
    using global::ProvaPub.Application.Services;
    using global::ProvaPub.Data.Repository;
    using global::ProvaPub.Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;
    using Xunit;


    public class CustomerServiceTests
    {
        private TestDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: dbName) // banco em memória, separado por teste
                .Options;

            return new TestDbContext(options);
        }

        [Fact]
        public async Task Should_Return_Error_When_CustomerId_Is_Invalid()
        {
            var ctx = GetDbContext(nameof(Should_Return_Error_When_CustomerId_Is_Invalid));
            var service = new CustomerService(ctx);

            var result = await service.CanPurchase(0, 50);

            Assert.False(result.Success);
            Assert.Equal("Cliente inválido", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_When_PurchaseValue_Is_Invalid()
        {
            var ctx = GetDbContext(nameof(Should_Return_Error_When_PurchaseValue_Is_Invalid));
            var service = new CustomerService(ctx);

            var result = await service.CanPurchase(1, 0);

            Assert.False(result.Success);
            Assert.Equal("Valor inválido", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_When_Customer_Does_Not_Exist()
        {
            var ctx = GetDbContext(nameof(Should_Return_Error_When_Customer_Does_Not_Exist));
            var service = new CustomerService(ctx);

            var result = await service.CanPurchase(99, 50);

            Assert.False(result.Success);
            Assert.Contains("does not exists", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_When_Customer_Already_Bought_This_Month()
        {
            var ctx = GetDbContext(nameof(Should_Return_Error_When_Customer_Already_Bought_This_Month));

            var customer = new Customer { Id = 1, Name = "Test" };
            ctx.Customers.Add(customer);
            ctx.Orders.Add(new Order
            {
                CustomerId = 1,
                OrderDate = DateTime.UtcNow.AddDays(-2),
                Value = 50
            });
            ctx.SaveChanges();

            var service = new CustomerService(ctx);
            var result = await service.CanPurchase(1, 50);

            Assert.False(result.Success);
            Assert.Equal("Usuário já fez uma compra este mês.", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_When_First_Purchase_Exceeds_100()
        {
            var ctx = GetDbContext(nameof(Should_Return_Error_When_First_Purchase_Exceeds_100));

            var customer = new Customer { Id = 1, Name = "Test" };
            ctx.Customers.Add(customer);
            ctx.SaveChanges();

            var service = new CustomerService(ctx);
            var result = await service.CanPurchase(1, 150);

            Assert.False(result.Success);
            Assert.Equal("Usuário não pode realizar compra com valor superior a R$100,00", result.Message);
        }

        [Fact]
        public async Task Should_Allow_Purchase_When_All_Rules_Are_Satisfied()
        {
            var ctx = GetDbContext(nameof(Should_Allow_Purchase_When_All_Rules_Are_Satisfied));

            var customer = new Customer { Id = 1, Name = "Test" };
            ctx.Customers.Add(customer);
            ctx.SaveChanges();

            var service = new CustomerService(ctx);

            // ⚠️ Esse teste só vai passar se rodar em horário comercial (08h–18h, dia útil).
            var result = await service.CanPurchase(1, 50);

            Assert.True(result.Success);
            Assert.Equal("Cliente pode realizar a compra.", result.Message);
            Assert.True(result.Response.CanPurchase);
        }
    }

}
