using Microsoft.EntityFrameworkCore;

namespace EducationalProject.Models
{
    public class ProjectContext : DbContext
    {

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Record> Records { get; set; }

        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base (options)
        {
            Database.EnsureCreated();
        }

    }
}
