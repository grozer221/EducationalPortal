using EducationalPortal.Server.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual T GetById(Guid? id)
        {
            T? entity = GetByIdOrDefault(id);
            if (entity == null)
                throw new Exception($"Не знайдено {typeof(T).Name.Replace("Model", "")}");
            return entity;
        }
        
        public virtual T GetByIdOrDefault(Guid? id)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        public virtual IEnumerable<T> Get()
        {
            IEnumerable<T> entities = GetOrDefault();
            if (entities == null)
                throw new Exception($"Не знайдено {typeof(T).Name.Replace("Model", "")}");
            return entities;
        }
        
        public virtual IEnumerable<T> GetOrDefault()
        {
            return _context.Set<T>().AsNoTracking();
        }
        
        public virtual IEnumerable<T> Get(Func<T, bool> condition)
        {
            IEnumerable<T> entities = GetOrDefault(condition);
            if (entities == null || entities.Count() == 0)
                throw new Exception($"Не знайдено {typeof(T).Name.Replace("Model", "")}");
            return entities;
        }
        
        public virtual IEnumerable<T> GetOrDefault(Func<T, bool> condition)
        {
            return _context.Set<T>().AsNoTracking().Where(condition); 
        }

        public virtual GetEntitiesResponse<T> Get(Func<T, object> predicate, bool descending, int page, Func<T, bool>? condition = null)
        {
            GetEntitiesResponse<T> getEntitiesResponse = GetOrDefault(predicate, descending, page, condition);
            if (getEntitiesResponse == null || getEntitiesResponse.Total == 0)
                throw new Exception($"Не знайдено {typeof(T).Name.Replace("Model", "")}");
            return getEntitiesResponse;
        }
        
        public virtual GetEntitiesResponse<T> GetOrDefault(Func<T, object> predicate, bool descending, int page, Func<T, bool>? condition = null)
        {
            IEnumerable<T> entities = descending
                ? _context.Set<T>().AsNoTracking().OrderByDescending(predicate)
                : _context.Set<T>().AsNoTracking().OrderBy(predicate);

            if (condition != null)
                entities = entities.Where(condition);

            int total = entities.Count();

            int take = 20;
            int skip = (page - 1) * take;
            entities = entities.Skip(skip).Take(take);

            return new GetEntitiesResponse<T>
            {
                Entities = entities,
                Total = total,
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
