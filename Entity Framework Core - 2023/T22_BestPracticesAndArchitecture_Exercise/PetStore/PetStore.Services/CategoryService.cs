using Microsoft.EntityFrameworkCore;
using PetStore.Data;
using PetStore.Data.Common.Models;
using PetStore.Data.Models;
using PetStore.Services.Interfaces;

namespace PetStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories
                .ToListAsync();
        }

        public async Task Create(string name)
        {
            var newCategory = new Category
            {
                Name = name
            };

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(int id, string name)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c=>c.Id ==id);

            category.Name = name;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
