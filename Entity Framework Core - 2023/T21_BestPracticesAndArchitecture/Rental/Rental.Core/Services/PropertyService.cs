using System;
using Rental.Core.Contracts;
using Rental.Core.Models;
using Rental.Infrastructure.Data.Common;
using Rental.Infrastructure.Data.Models;

namespace Rental.Core.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IRepository repository;

        public PropertyService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<int> CreateAsync(PropertyModel model)
        {
            Property property = new Property
            {
                Area = model.Area,
                Location = model.Location,
                Price = model.Price
            };

            await repository.AddAsync(property);
            await repository.SaveChangesAsync();

            return property.Id;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PropertyModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PropertyModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, PropertyModel model)
        {
            throw new NotImplementedException();
        }
    }
}

