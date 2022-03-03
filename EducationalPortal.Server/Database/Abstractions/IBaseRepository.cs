using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Server.Database.Abstractions
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(Guid? id);
        //IEnumerable<T> Get(Func<T, object> predicate, bool reverse);
        //GetEntitiesResponse<T> Get(int page, Func<T, object> predicate, bool reverse);
        //IEnumerable<T> Get(Func<T, bool> condition, Func<T, object> predicate, bool reverse);
        IEnumerable<T> Get();
        IEnumerable<T> Get(Func<T, bool> condition);
        GetEntitiesResponse<T> Get(Func<T, object> predicate, bool descending, int page, Func<T, bool>? condition);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task RemoveAsync(Guid id);
    }
}
