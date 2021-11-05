using Microsoft.EntityFrameworkCore;
using MyModelAndDatabase.Models;

namespace MyModelAndDatabase.Data.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            :base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
