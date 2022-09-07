namespace Company.Project.Domain.Interfaces.Generics.Base
{
    using Entities.Generics;
    using Entities.Generics.Base;
    using System.Collections.Generic;

    /// <summary>
    /// Base service interface. 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IBaseService<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool Create(TEntity entity);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> Read();

        /// <summary>
        /// Reads the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Read(System.Func<TEntity, bool> filter);

        /// <summary>
        /// Reads by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        TEntity Read(int id);

        /// <summary>
        /// Reads with paged results.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="isAsc">if set to <c>true</c> [is asc].</param>
        /// <returns></returns>
        Page<TEntity> Read(int pageIndex, int pageSize, string? sortBy = null, bool isAsc = true);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool Update(TEntity entity);

        /// <summary>
        /// Deletes by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        bool Delete(int id);
    }
}
