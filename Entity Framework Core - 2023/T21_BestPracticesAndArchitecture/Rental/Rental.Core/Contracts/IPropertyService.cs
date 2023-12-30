using System;
using Rental.Core.Models;

namespace Rental.Core.Contracts
{
    public interface IPropertyService
    {
        Task<int> CreateAsync(PropertyModel model);

        Task<IEnumerable<PropertyModel>> GetAllAsync();

        Task<PropertyModel> GetByIdAsync(int id);

        Task UpdateAsync(int id, PropertyModel model);

        Task DeleteAsync(int id);
    }
}

