using EducationalPortal.Server.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Server.Database.Abstractions
{
    public class BaseRepository<T> where T : BaseModel
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public virtual T GetById(Guid? id, params Expression<Func<T, object>>[] includes)
        {
            T? entity = GetByIdOrDefault(id, includes);
            if (entity == null)
                throw new Exception($"Не знайдено {typeof(T).Name.Replace("Model", "")}");
            return entity;
        }
        
        public virtual T? GetByIdOrDefault(Guid? id, params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate(
              _context.Set<T>().AsQueryable(),
              (current, include) => current.Include(include)
            ).FirstOrDefault(e => e.Id == id);
        }

        public virtual List<T> Get(params Expression<Func<T, object>>[] includes)
        {
            List<T> entities = GetOrDefault(includes);
            if (entities == null)
                throw new Exception($"Не знайдено {typeof(T).Name.Replace("Model", "")}");
            return entities;
        }
        
        public virtual List<T> GetOrDefault(params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate(
               _context.Set<T>().AsQueryable(),
               (current, include) => current.Include(include)
            ).ToList();
        }
        
        public virtual List<T> Get(Func<T, bool> condition, params Expression<Func<T, object>>[] includes)
        {
            List<T> entities = GetOrDefault(condition, includes);
            if (entities == null || entities.Count() == 0)
                throw new Exception($"Не знайдено {typeof(T).Name.Replace("Model", "")}");
            return entities;
        }
        
        public virtual List<T> GetOrDefault(Func<T, bool> condition, params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate(
                _context.Set<T>().AsQueryable(),
                (current, include) => current.Include(include)
            ).Where(condition).ToList();
        }

        public virtual GetEntitiesResponse<T> Get(Func<T, object> predicate, Order order, int page, Func<T, bool>? condition = null, params Expression<Func<T, object>>[] includes)
        {
            GetEntitiesResponse<T> getEntitiesResponse = GetOrDefault(predicate, order, page, condition, includes);
            if (getEntitiesResponse == null || getEntitiesResponse.Total == 0)
                throw new Exception($"Не знайдено {typeof(T).Name.Replace("Model", "")}");
            return getEntitiesResponse;
        }
        
        public virtual GetEntitiesResponse<T> GetOrDefault(Func<T, object> predicate, Order order, int page, Func<T, bool>? condition = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T>? includedQuery = includes.Aggregate(
                _context.Set<T>().AsQueryable(),
                (current, include) => current.Include(include)
            );
            IEnumerable<T> entities = order == Order.Ascend
                ? includedQuery.OrderBy(predicate)
                : order == Order.Descend
                    ? includedQuery.OrderByDescending(predicate)
                    : new List<T>();

            if (condition != null)
                entities = entities.Where(condition);

            int total = entities.Count();

            int take = 20;
            int skip = (page - 1) * take;
            entities = entities.Skip(skip).Take(take);

            return new GetEntitiesResponse<T>
            {
                Entities = entities.ToList(),
                Total = total,
                PageSize = take,
            };
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task RemoveAsync(Guid id)
        {
            T entity = GetById(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
