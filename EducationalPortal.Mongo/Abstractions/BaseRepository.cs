using EducationalPortal.Business.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace EducationalPortal.Mongo.Abstractions
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly IMongoCollection<T> entitiesCollection;

        public BaseRepository()
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(Environment.GetEnvironmentVariable("ATLAS_URI"));
            settings.LinqProvider = LinqProvider.V3;
            var mongoClient = new MongoClient(settings);
            var mongoDatabase = mongoClient.GetDatabase("EducationalPortal");
            entitiesCollection = mongoDatabase.GetCollection<T>(typeof(T).Name);
        }

        public virtual Task<T> GetByIdAsync(Guid? id, params Expression<Func<T, object>>[] includes)
        {
            Task<T?> entity = GetByIdOrDefaultAsync(id, includes);
            if (entity == null)
                throw new Exception($"Не знайдено {typeof(T).Name.Replace("Model", "")}");
            return entity;
        }

        public virtual Task<T?> GetByIdOrDefaultAsync(Guid? id, params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate(entitiesCollection.AsQueryable(), (current, include) => current)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual Task<List<T>> GetAsync(params Expression<Func<T, object>>[] includes)
        {
            Task<List<T>> entities = GetOrDefaultAsync(includes);
            if (entities == null)
                throw new Exception($"Не знайдено {typeof(T).Name.Replace("Model", "")}");
            return entities;
        }

        public virtual Task<List<T>> GetOrDefaultAsync(params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate(entitiesCollection.AsQueryable(), (current, include) => current)
                .ToListAsync();
        }

        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            List<T> entities = await GetOrDefaultAsync(condition, includes);
            if (entities == null || entities.Count() == 0)
                throw new Exception($"Не знайдено {typeof(T).Name.Replace("Model", "")}");
            return entities;
        }

        public virtual Task<List<T>> GetOrDefaultAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate(entitiesCollection.AsQueryable(), (current, include) => current)
                .Where(condition)
                .ToListAsync();
        }

        public virtual async Task<GetEntitiesResponse<T>> WhereAsync(Expression<Func<T, object>> predicate, Order order, int page, Expression<Func<T, bool>>? condition = null, params Expression<Func<T, object>>[] includes)
        {
            GetEntitiesResponse<T> getEntitiesResponse = await WhereOrDefaultAsync(predicate, order, page, condition, includes);
            if (getEntitiesResponse == null || getEntitiesResponse.Total == 0)
                throw new Exception($"Не знайдено {typeof(T).Name.Replace("Model", "")}");
            return getEntitiesResponse;
        }

        public virtual async Task<GetEntitiesResponse<T>> WhereOrDefaultAsync(Expression<Func<T, object>> predicate, Order order, int page, Expression<Func<T, bool>>? condition = null, params Expression<Func<T, object>>[] includes)
        {
            var entities = includes.Aggregate(entitiesCollection.AsQueryable(), (current, include) => current);

            if (condition != null)
                entities = entities.Where(condition);

            entities = order == Order.Ascend
                ? entities.OrderBy(predicate)
                : entities.OrderByDescending(predicate);

            int total = entities.Count();


            int take = 20;
            int skip = (page - 1) * take;
            entities = entities.Skip(skip).Take(take);

            return new GetEntitiesResponse<T>
            {
                Entities = await entities.ToListAsync(),
                Total = total,
                PageSize = take,
            };
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            await entitiesCollection.InsertOneAsync(entity);
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            await entitiesCollection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
            return entity;
        }

        public virtual Task RemoveAsync(Guid id)
        {
            return entitiesCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
