namespace Company.Project.Application.Interfaces.Generics.Base
{
    using Domain.Entities.Generics;
    using Domain.Entities.Generics.Base;
    using System.Collections.Generic;

    /// <summary>
    /// Base Application interface. 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IBaseApplication<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Response<bool> Create(TEntity entity);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        Response<IEnumerable<TEntity>> Read();

        /// <summary>
        /// Reads by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Response<TEntity> Read(int id);

        /// <summary>
        /// Reads with paged results.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="isAsc">if set to <c>true</c> [is asc].</param>
        /// <returns></returns>
        Response<Page<TEntity>> Read(int pageIndex, int pageSize, string sortBy = null, bool isAsc = true);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Response<bool> Update(TEntity entity);

        /// <summary>
        /// Deletes by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Response<bool> Delete(int id);
    }
}
