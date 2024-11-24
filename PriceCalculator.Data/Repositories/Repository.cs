using Microsoft.EntityFrameworkCore;
using PriceCalculator.Data.Data;
using PriceCalculator.Data.Interfaces;
using System.Linq.Expressions;

namespace PriceCalculator.Data.Repositories
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

        public async Task<IEnumerable<T>> GetAllAsync(
    Expression<Func<T, bool>>? filter = null,
    string? includeProperties = null,
    bool tracked = false)
        {
            IQueryable<T> dbSetQuery = tracked ? dbSet : dbSet.AsNoTracking();

            // Split the includeProperties string and add each one to the query
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    dbSetQuery = dbSetQuery.Include(includeProperty.Trim());
                }
            }

            if (filter != null)
            {
                dbSetQuery = dbSetQuery.Where(filter);
            }

            return await dbSetQuery.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsyncWithPaging(Expression<Func<T, bool>>? filter = null, bool includeProperties = false, bool tracked = false)
        {
            var pageSize = 5;

            var skip = 0;
            IQueryable<T> dbSetQuery = tracked ? dbSet : dbSet.AsNoTracking();

            if (!includeProperties)
                dbSetQuery = dbSetQuery.IgnoreAutoIncludes();

            if (filter != null)
                dbSetQuery = dbSetQuery.Where(filter).AsSplitQuery();


            var data = dbSetQuery.Skip(skip).Take(pageSize);
            var recordsTotal = dbSetQuery.Count();

            return await data.ToListAsync();
        }

        public async Task<T?> GetAsync(
    Expression<Func<T, bool>> filter,
    string? includeProperties = null,
    bool tracked = false)
        {
            IQueryable<T> dbSetQuery = tracked ? dbSet : dbSet.AsNoTracking();

            // If includeProperties is specified, include each property in the query
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    dbSetQuery = dbSetQuery.Include(includeProperty.Trim());
                }
            }
            else
            {
                dbSetQuery = dbSetQuery.IgnoreAutoIncludes();
            }

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
