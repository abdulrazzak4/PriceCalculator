﻿using System.Linq.Expressions;

namespace PriceCalculator.App.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool includeProperties = false, bool tracked = false);
        Task<IEnumerable<T>> GetAllAsyncWithPaging(Expression<Func<T, bool>>? filter = null, bool includeProperties = false, bool tracked = false);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, bool includeProperties = true, bool tracked = false);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task SaveAsync();
    }
}
