using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Database.Abstractions
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid? id);
        IEnumerable<T> Get();
        IEnumerable<T> Get(int page);
        IEnumerable<T> Get(Func<T, bool> condition);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task RemoveAsync(Guid id);
    }
}
