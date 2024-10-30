using Microsoft.EntityFrameworkCore;
using PriceCalculator.App.Data;
using PriceCalculator.App.Interfaces;
using System.Linq.Expressions;

namespace PriceCalculator.App.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            this.db = db;
            this.dbSet = db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await Task.FromResult(dbSet.Add(entity));
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool includeProperties = false, bool tracked = false) 
        {
            IQueryable<T> dbSetQuery = tracked ? dbSet : dbSet.AsNoTracking();
            
            if(!includeProperties)
                dbSetQuery = dbSetQuery.IgnoreAutoIncludes();

            if (filter != null)
                dbSetQuery = dbSetQuery.Where(filter).AsSplitQuery();

            return await dbSetQuery.ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsyncWithPaging(Expression<Func<T, bool>>? filter = null, bool includeProperties = false, bool tracked = false) 
        {
            var pageSize = 5;

            var skip = 0;
            IQueryable<T> dbSetQuery = tracked ? dbSet : dbSet.AsNoTracking();
            
            if(!includeProperties)
                dbSetQuery = dbSetQuery.IgnoreAutoIncludes();

            if (filter != null)
                dbSetQuery = dbSetQuery.Where(filter).AsSplitQuery();


            var data = dbSetQuery.Skip(skip).Take(pageSize);
            var recordsTotal = dbSetQuery.Count();

            return await data.ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, bool includeProperties = true, bool tracked = false)
        {
            IQueryable<T> dbSetQuery = tracked ? dbSet : dbSet.AsNoTracking();

            if (!includeProperties)
                dbSetQuery = dbSetQuery.IgnoreAutoIncludes();

            return await dbSetQuery.FirstOrDefaultAsync(filter);
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.FromResult(dbSet.Remove(entity));
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.FromResult(dbSet.Update(entity));
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
