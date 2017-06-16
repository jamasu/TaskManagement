using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaskManagerDbDAL;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TaskManagment.Repository
{
    public class TaskRepository<T> : IRepository<T> where T: class
    {
        protected readonly TaskManagerDbContext _context;
        public TaskRepository(TaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return  _context.Set<T>().Where(predicate);
        }

        public T Get(int id)
        {
            return  _context.Set<T>().Find(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
              _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }

}
