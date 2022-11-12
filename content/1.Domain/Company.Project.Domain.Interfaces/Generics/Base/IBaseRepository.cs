namespace Company.Project.Domain.Interfaces.Generics.Base
{
    using Entities.Generics;
    using Entities.Generics.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Base Repository interface. 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<bool> Create(TEntity entity);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Read();

        /// <summary>
        /// Reads by the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Read(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Reads by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<TEntity?> Read(ulong id);

        /// <summary>
        /// Reads with paged results.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="isAsc">if set to <c>true</c> [is asc].</param>
        /// <returns></returns>
        Task<Page<TEntity>> Read(uint pageIndex, uint pageSize, string? sortBy = null, bool isAsc = true);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<bool> Update(TEntity entity);

        /// <summary>
        /// Deletes by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> Delete(ulong id);
    }
}
