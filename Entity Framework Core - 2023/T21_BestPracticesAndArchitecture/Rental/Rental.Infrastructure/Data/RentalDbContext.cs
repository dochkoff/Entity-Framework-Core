using System;
using Microsoft.EntityFrameworkCore;
using Rental.Infrastructure.Data.Models;

namespace Rental.Infrastructure.Data
{
    public class RentalDbContext : DbContext
    {
        public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; } = null!;
    }
}

