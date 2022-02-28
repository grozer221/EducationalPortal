using EducationalPortal.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Database.Abstractions
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<T> Get()
        {
            IEnumerable<T>? entities = _context.Set<T>();
            if (entities == null)
                throw new Exception("Не знайдено");
            return entities;
        }
        
        public virtual IEnumerable<T> Get(int page = 1)
        {
            int take = 20;
            int skip = (page - 1) * take;
            IEnumerable<T>? entities = _context.Set<T>().Skip(skip).Take(take);
            if(entities == null)
                throw new Exception("Не знайдено");
            return entities;
        }

        public virtual async Task<T> GetByIdAsync(Guid? id)
        {
            T? entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
                throw new Exception("Не знайдено");
            return entity;
        }

        public virtual IEnumerable<T> Get(Func<T, bool> condition)
        {
            IEnumerable<T>? entities = _context.Set<T>().Where(condition);
            if (entities == null)
                throw new Exception("Не знайдено");
            return entities;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
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
            T entity = await GetByIdAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
