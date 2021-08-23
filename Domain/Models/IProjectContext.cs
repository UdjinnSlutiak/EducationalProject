// <copyright file="IProjectContext.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Domain.Models
{
    using System.Linq;

    /// <summary>
    /// Provides neccessary ProjectContext properties and methods.
    /// </summary>
    public interface IProjectContext
    {
        /// <summary>
        /// Gets equipment objects queryable collection that replaces DbSet functional.
        /// </summary>
        IQueryable<Equipment> Equipments { get; }

        /// <summary>
        /// Gets user objects queryable collection that replaces DbSet functional.
        /// </summary>
        IQueryable<User> Users { get; }

        /// <summary>
        /// Gets record objects queryable collection that replaces DbSet functional.
        /// </summary>
        IQueryable<Record> Records { get; }

        /// <summary>
        /// Method-mediator that provides information to base DbContext Add method.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="entity">Entity instance.</param>
        void Add<T>(T entity);

        /// <summary>
        /// Method-mediator that provides information to base DbContext SaveChangesAsync method.
        /// </summary>
        void SaveChangesAsync();

        /// <summary>
        /// Method-mediator that provides information to base DbContext Update method.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="entity">Variable instance.</param>
        void Update<T>(T entity);

        /// <summary>
        /// Method-mediator that provides information to base DbContext Remove method.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="entity">Variable instance.</param>
        void Remove<T>(T entity);
    }
}
