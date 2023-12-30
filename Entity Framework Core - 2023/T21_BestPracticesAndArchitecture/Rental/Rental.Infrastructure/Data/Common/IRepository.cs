using System;
namespace Rental.Infrastructure.Data.Common
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(int id) where T : class;

        Task AddAsync<T>(T entity) where T : class;

        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class;

        Task DeleteAsync<T>(int id) where T : class;

        IQueryable<T> All<T>() where T : class;

        IQueryable<T> AllReadonly<T>() where T : class;

        Task SaveChangesAsync();
    }
}

