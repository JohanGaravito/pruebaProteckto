using Microsoft.EntityFrameworkCore;
using ProyectoTodoApi.Models;

namespace ProyectoTodoApi.Models
{
    public class todoDbContext : DbContext
    {
        public todoDbContext(DbContextOptions<todoDbContext> options) : base(options) { }

        public DbSet<todoitems> todoitems { get; set; }
       
    }
}
