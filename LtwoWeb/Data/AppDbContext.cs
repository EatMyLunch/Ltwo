using LtwoWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace LtwoWeb.Data
{
    public class AppDbContext : DbContext
    {
     
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<TrainingTitle> TrainingTitles { get; set; }
        public DbSet<TrainingType> TrainingTypes { get; set; }

    }
}
