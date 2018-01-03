using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace MyMovieManager.Models
{
    class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["MyMovieManagerConnection"].ConnectionString);
        }
    }
}
