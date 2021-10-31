using HepsiBuradaCaseStudy.Domain.Entities;
using HepsiBuradaCaseStudy.Infrastructure.Data.Context;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace HepsiBuradaCaseStudy.Infrastructure.Repository
{
    public class ProductRepository : MongoRepository<Product>, IProductRepository
    {
        public ProductRepository(ICoreContext context) : base(context)
        {
        }
    }
}
