using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{
    public class ProjectContext : DbContext
    {

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Record> Records { get; set; }

        public ProjectContext()
        {
            Database.EnsureCreated();
        }

        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base (options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433; Database=ProjectDB; User=sa; Password=KAnITOWKA13");
            }
        }
    }
}
