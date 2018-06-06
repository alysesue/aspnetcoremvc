using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GrandeTravelMVC.Services
{
    public class DataService<T> : IDataService<T> where T : class
    {
        private MyDbContext _context;
        private DbSet<T> _dbset;

        public DataService()
        {
            _context = new MyDbContext();
            _dbset = _context.Set<T>();
        }

        public void Create(T entity)
        {
            _dbset.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
            _context.SaveChanges();
        }
    }
}
