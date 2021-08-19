// <copyright file="ProjectContext.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Domain.Models
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Creates DbSets, initializes database.
    /// </summary>
    public class ProjectContext : DbContext, IProjectContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectContext"/> class. Ensures that database is created.
        /// </summary>
        public ProjectContext()
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectContext"/> class. Ensures that database is created, receives and sends ContextOptions to the base DbContext class.
        /// </summary>
        /// <param name="options">Context options instance.</param>
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Gets or sets equipments instances to DbSet.
        /// </summary>
        public DbSet<Equipment> Equipments { get; set; }

        /// <summary>
        /// Gets or sets users instances to DbSet.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets records instances to DbSet.
        /// </summary>
        public DbSet<Record> Records { get; set; }

        /// <summary>
        /// Provides data from queryable equipment collection to DbSet.
        /// </summary>
        IQueryable<Equipment> IProjectContext.Equipments => this.Equipments;

        /// <summary>
        /// Provides data from queryable user collection to DbSet.
        /// </summary>
        IQueryable<User> IProjectContext.Users => this.Users;

        /// <summary>
        /// Provides data from queryable record collection to DbSet.
        /// </summary>
        IQueryable<Record> IProjectContext.Records => this.Records;

        /// <summary>
        /// Calls base DbContext Add method and pass entity instance.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="entity">Entity instance.</param>
        void IProjectContext.Add<T>(T entity)
        {
            this.Add(entity);
        }

        /// <summary>
        /// Calls base DbContext SaveChangesAsync method.
        /// </summary>
        void IProjectContext.SaveChangesAsync()
        {
            this.SaveChangesAsync();
        }

        /// <summary>
        /// Calls base DbContext Update method and pass entity instance.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="entity">Entity instance.</param>
        void IProjectContext.Update<T>(T entity)
        {
            this.Update(entity);
        }

        /// <summary>
        /// Calls base DbContext Remove method and pass entity instance.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="entity">Entity instance.</param>
        void IProjectContext.Remove<T>(T entity)
        {
            this.Remove(entity);
        }

        /// <summary>
        /// Configures which server DbContext have to use and sets connection string.
        /// </summary>
        /// <param name="optionsBuilder">Context options builder instance.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433; Database=ProjectDB; User=sa; Password=KAnITOWKA13");
            }
        }
    }
}
