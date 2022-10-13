using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models
{
    public class PizzeriaContext : IdentityDbContext<IdentityUser>
    {
        public PizzeriaContext()
        {

        }

        public PizzeriaContext(DbContextOptions<PizzeriaContext> options) : base(options)
        {

        }
        public DbSet<Pizza> Pizze { get; set; } 

        public DbSet<Category> Category { get; set; }
        
        public DbSet<Ingredient> Ingredients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=la-mia-pizzeria-db;Integrated Security=True");
        }
    }
}
