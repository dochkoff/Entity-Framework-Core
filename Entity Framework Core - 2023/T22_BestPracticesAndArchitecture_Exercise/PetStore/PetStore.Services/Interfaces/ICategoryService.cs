using PetStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Services.Interfaces
{
    public interface ICategoryService
    {
        Task Create(string name);
        Task DeleteById(int id);
        Task<IEnumerable<Category>> GetAll();
        Task<Category?> GetById(int id);
        Task Update(int id, string name);
    }
}
