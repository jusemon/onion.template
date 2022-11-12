namespace Company.Project.Application.Services.Generics.Base
{
    using Interfaces.Generics;
    using Interfaces.Generics.Base;
    using Domain.Entities.Generics;
    using Domain.Entities.Generics.Base;
    using Domain.Interfaces.Generics.Base;
    using System.Collections.Generic;

    /// <summary>
    /// Base Application class. 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Company.Project.Application.Interfaces.Generics.Base.IBaseApplication{TEntity}" />
    public class BaseApplication<TEntity> : IBaseApplication<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// The base service
        /// </summary>
        private readonly IBaseService<TEntity> baseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApplication{TEntity}"/> class.
        /// </summary>
        /// <param name="baseService">The base service.</param>
        public BaseApplication(IBaseService<TEntity> baseService)
        {
            this.baseService = baseService;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual Task<Response<bool>> Create(TEntity entity)
        {
            return ApplicationExtensions.TryAsync(() => this.baseService.Create(entity));
        }

        /// <summary>
        /// Deletes by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual Task<Response<bool>> Delete(ulong id)
        {
            return ApplicationExtensions.TryAsync(() => this.baseService.Delete(id));
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public virtual Task<Response<IEnumerable<TEntity>>> Read()
        {
            return ApplicationExtensions.TryAsync(() => this.baseService.Read());
        }

        /// <summary>
        /// Reads by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual Task<Response<TEntity?>> Read(ulong id)
        {
            return ApplicationExtensions.TryAsync(() => this.baseService.Read(id));
        }

        /// <summary>
        /// Reads with paged results.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="isAsc">if set to <c>true</c> [is asc].</param>
        /// <returns></returns>
        public virtual Task<Response<Page<TEntity>>> Read(uint pageIndex, uint pageSize, string? sortBy = null, bool isAsc = true)
        {
            return ApplicationExtensions.TryAsync(() => this.baseService.Read(pageIndex, pageSize, sortBy, isAsc));
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual Task<Response<bool>> Update(TEntity entity)
        {
            return ApplicationExtensions.TryAsync(() => this.baseService.Update(entity));
        }
    }
}
