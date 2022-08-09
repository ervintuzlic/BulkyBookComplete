using BulkyBookComplete.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookComplete.DataAccess
{
    //** Inherit from DbContext and add package Microsoft.EntityFrameworkCore
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CoverType> CoverTypes { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
