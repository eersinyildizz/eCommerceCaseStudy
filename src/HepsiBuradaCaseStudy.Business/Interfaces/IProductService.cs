using System.Collections.Generic;
using System.Threading.Tasks;
using HepsiBuradaCaseStudy.Domain.Entities;

namespace HepsiBuradaCaseStudy.Business.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByCodeAsync(string code);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetProductByIdAsync(string id);

        Task<Product> AddAsync(Product product);

        Task<Product> UpdateAsync(Product product);

        Task StockControl(string productCode, int orderQuantity);
    }
}
