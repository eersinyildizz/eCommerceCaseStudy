using HepsiBuradaCaseStudy.Domain.Entities;
using MongoDB.Driver;

namespace HepsiBuradaCaseStudy.Infrastructure.Data.Context
{
    public interface ICoreContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
