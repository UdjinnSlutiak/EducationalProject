using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{
    public class ProjectContext : DbContext, IProjectContext
    {

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Record> Records { get; set; }

        IQueryable<Equipment> IProjectContext.Equipments => this.Equipments;

        IQueryable<User> IProjectContext.Users => this.Users;

        IQueryable<Record> IProjectContext.Records => this.Records;

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

        void IProjectContext.Add<T>(T entity)
        {
            base.Add(entity);
        }

        void IProjectContext.SaveChangesAsync()
        {
            base.SaveChangesAsync();
        }

        void IProjectContext.Update<T>(T entity)
        {
            base.Update(entity);
        }

        void IProjectContext.Remove<T>(T entity)
        {
            base.Remove(entity);
        }
    }
}
