using HepsiBuradaCaseStudy.Domain.Entities;
using HepsiBuradaCaseStudy.Infrastructure.Data.Context;

namespace HepsiBuradaCaseStudy.Infrastructure.Repository
{
    public class OrderRepository : MongoRepository<Order>,IOrderRepository
    {
        public OrderRepository(ICoreContext context) : base(context)
        {
        }
    }
}
