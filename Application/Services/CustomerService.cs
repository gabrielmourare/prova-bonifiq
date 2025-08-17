using Microsoft.EntityFrameworkCore;
using ProvaPub.API.DTOs;
using ProvaPub.API.DTOs.Responses;
using ProvaPub.Application.Interfaces;
using ProvaPub.Data.Repository;
using ProvaPub.Domain.Models;

namespace ProvaPub.Application.Services
{
    public class CustomerService : ICustomerService
    {
        TestDbContext _ctx;
        IPaginationService _paginationService;

        public CustomerService(TestDbContext ctx)
        {
            _ctx = ctx;
        }
        public CustomerService(TestDbContext ctx, IPaginationService paginationService)
        {
            _ctx = ctx;
            _paginationService = paginationService;
        }

        async Task<(bool Success, string Message, PagedResult<Customer> Response)> IBaseService<Customer>.List(int page, int pageSize)

        {
            var query = _ctx.Customers.OrderBy(c => c.Id);
            var data = await _paginationService.PaginateAsync(query, page, pageSize);

            if (data == null)
            {
                return (false, "Erro ao paginar", data);
            }
            return (true, string.Empty, data);
        }

        public async Task<(bool Success, string Message, CustomerResponseDTO Response)> CanPurchase(int customerId, decimal purchaseValue)
        {
            CustomerResponseDTO response = new CustomerResponseDTO();

            if (customerId <= 0) return (false, "Cliente inválido", response);

            if (purchaseValue <= 0) return (false, "Valor inválido", response);

            //Business Rule: Non registered Customers cannot purchase
            var customer = await _ctx.Customers.FindAsync(customerId);
            if (customer == null) return (false, $"Customer Id {customerId} does not exists", response);

            //Business Rule: A customer can purchase only a single time per month
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await _ctx.Orders.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            if (ordersInThisMonth > 0)
                return (false, "Usuário já fez uma compra este mês.", response);

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = await _ctx.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return (false, "Usuário não pode realizar compra com valor superior a R$100,00", response);

            //Business Rule: A customer can purchases only during business hours and working days
            if (DateTime.UtcNow.Hour < 8 || DateTime.UtcNow.Hour > 18 || DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday || DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday)
                return (false, "Usuário só pode realizar compras em dias úteis e horário comercial", response);

            response.CanPurchase = true;
            response.CustomerId = customerId;

            return (true, "Cliente pode realizar a compra.", response);
        }

        public async Task<(bool Success, string Message, Customer Response)> GetById(int customerId)
        {
            Customer customer = await _ctx.Customers
                                .Where(c => c.Id == customerId)
                                .FirstOrDefaultAsync();

            if (customer == null)
                return (false, "Cliente não encontrado", new Customer());

            return (true, "Cliente encontrado", customer);
        }
    }
}
