using Microsoft.EntityFrameworkCore;
using MyModelAndDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelAndDatabase.Data.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
