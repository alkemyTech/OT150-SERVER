using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OngProject.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly OngContext _context;
        protected readonly DbSet<T> _entities;
        public Repository(OngContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable().Where(x => x.SoftDelete == true);
        }
        public T GetById(int id)
        {
            return _entities.FirstOrDefault(x => x.Id == id && x.SoftDelete == true);
        }
        public void Add(T entity)
        {
            _entities.AddAsync(entity);
        }
        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public async Task<T> Delete(int id)
        {
            T entidad = await _entities.FirstOrDefaultAsync(x => x.Id == id && x.SoftDelete == true);
            if (entidad == null)
            {
                return entidad;
            }
            entidad.SoftDelete = false;
            entidad.LastModified = DateTime.Now;
            return entidad;
        }

        public async Task<ICollection<T>> FindAllAsync(
             Expression<Func<T, bool>> filter = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             IList<Expression<Func<T, object>>> includes = null,
             int? page = null,
             int? pageSize = null)
        {
            var query = _entities.AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return await query.ToListAsync();
        }

        public async Task<int> Count()
        {
            return await _entities.CountAsync();
        }

    }
}