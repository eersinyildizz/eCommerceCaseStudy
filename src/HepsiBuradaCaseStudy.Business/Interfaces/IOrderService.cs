using System.Collections.Generic;
using System.Threading.Tasks;
using HepsiBuradaCaseStudy.Domain.Entities;

namespace HepsiBuradaCaseStudy.Business.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order> GetOrderByIdAsync(string id);

        Task<Order> AddOrUpdateAsync(Order order);

        Task<Order> GetOrderByProductCodeAsync(string productCode);
    }
}
