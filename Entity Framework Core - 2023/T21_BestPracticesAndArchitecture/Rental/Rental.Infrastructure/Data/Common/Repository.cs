using System;
using Microsoft.EntityFrameworkCore;

namespace Rental.Infrastructure.Data.Common
{
    public class Repository : IRepository
    {
        private readonly RentalDbContext context;

        public Repository(RentalDbContext _context)
        {
            context = _context;
        }

        private DbSet<T> GetDbSet<T>() where T : class
        {
            return context.Set<T>();
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await GetDbSet<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await GetDbSet<T>().AddRangeAsync(entities);
        }

        public IQueryable<T> All<T>() where T : class
        {
            return GetDbSet<T>()
                .AsQueryable();
        }

        public IQueryable<T> AllReadonly<T>() where T : class
        {
            return GetDbSet<T>()
                .AsNoTracking();
        }

        public async Task DeleteAsync<T>(int id) where T : class
        {
            T? entity = await GetDbSet<T>().FindAsync(id);

            if (entity != null)
            {
                GetDbSet<T>().Remove(entity);
            }
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : class
        {
            return await GetDbSet<T>().FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}

