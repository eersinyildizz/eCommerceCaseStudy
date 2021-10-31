using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HepsiBuradaCaseStudy.Domain.Entities;
using HepsiBuradaCaseStudy.Infrastructure.Data.Context;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HepsiBuradaCaseStudy.Infrastructure.Repository
{
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ICoreContext context;
        protected readonly IMongoCollection<TEntity> Collection;

        public MongoRepository(ICoreContext context)
        {
            this.context = context;
            Collection = this.context.GetCollection<TEntity>(typeof(TEntity).Name.ToLowerInvariant());
        }

        public async ValueTask<TEntity> GetByIdAsync(string id)
        {
            return await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await Collection.AsQueryable().ToListAsync();
        }

        public async Task<List<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.AsQueryable().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate).FirstOrDefaultAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await Collection.InsertOneAsync(entity, options);
            return entity;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            return (await Collection.BulkWriteAsync((IEnumerable<WriteModel<TEntity>>)entities, options)).IsAcknowledged;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            return await Collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
        }
    }
}
