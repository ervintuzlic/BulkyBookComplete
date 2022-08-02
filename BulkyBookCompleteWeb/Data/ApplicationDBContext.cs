using BulkyBookCompleteWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookCompleteWeb.Data
{
    //** Inherit from DbContext and add package Microsoft.EntityFrameworkCore
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }


    }
}
