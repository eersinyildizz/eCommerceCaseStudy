using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using HepsiBuradaCaseStudy.Domain.Entities;
using HepsiBuradaCaseStudy.Domain.Settings;
using MongoDB.Driver;

namespace HepsiBuradaCaseStudy.Infrastructure.Data.Context
{
    public class CoreContext : ICoreContext
    {
        private readonly IMongoClient mongoClient;
        private readonly IMongoDatabase database;
        public CoreContext(IOptions<MongoDbSettings> configuration)
        {
            mongoClient = new MongoClient(configuration.Value.ConnectionString);
            database = mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return database.GetCollection<T>(name);
        }
    }
}
