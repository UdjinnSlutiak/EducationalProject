// <copyright file="ProjectContext.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Domain.Models
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Creates DbSets, initializes database.
    /// </summary>
    public class ProjectContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectContext"/> class. Ensures that database is created.
        /// </summary>
        public ProjectContext() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectContext"/> class. Ensures that database is created, receives and sends ContextOptions to the base DbContext class.
        /// </summary>
        /// <param name="options">Context options instance.</param>
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options) { }

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
    }
}
