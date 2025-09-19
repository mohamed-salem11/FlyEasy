using FlyEasy.Data;
using FlyEasy.Repositories.Implementations;
using FlyEasy.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlyEasy.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected readonly FlyEasyContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(FlyEasyContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

    }

}


















