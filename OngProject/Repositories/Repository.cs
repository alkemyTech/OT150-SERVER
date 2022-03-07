using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}