using System.Collections.Generic;
using System.Threading.Tasks;
using HepsiBuradaCaseStudy.Business.Interfaces;
using HepsiBuradaCaseStudy.Domain.Entities;
using HepsiBuradaCaseStudy.Infrastructure.Repository;

namespace HepsiBuradaCaseStudy.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repository;
        public OrderService(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Order> AddOrUpdateAsync(Order order)
        {
            var orderEntity = await GetOrderByProductCodeAsync(order.ProductCode);
            if (orderEntity != null)
            {
                orderEntity.Quantity += order.Quantity;
                await repository.UpdateAsync(orderEntity);
                return await GetOrderByIdAsync(orderEntity.Id);
            }
            else
            {
                await repository.AddAsync(order);
                return await GetOrderByIdAsync(order.Id);
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(string id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<Order> GetOrderByProductCodeAsync(string productCode)
        {
            return await repository.FirstOrDefaultAsync(o => o.ProductCode == productCode);
        }
    }
}
