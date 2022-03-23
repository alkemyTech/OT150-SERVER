using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        Task<T> Delete(int id);
        Task<ICollection<T>> FindAllAsync(
             Expression<Func<T, bool>> filter = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             IList<Expression<Func<T, object>>> includes = null,
             int? page = null,
             int? pageSize = null);
        Task<int> Count();
        Task<T> GetByIdAsync(int id);
    }
}
