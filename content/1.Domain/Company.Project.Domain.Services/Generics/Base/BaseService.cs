namespace Company.Project.Domain.Services.Generics.Base
{
    using Entities.Generics;
    using Entities.Generics.Base;
    using FluentValidation;
    using Infra.Utils.Exceptions;
    using Interfaces.Generics.Base;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Base Service class. 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Company.Project.Domain.Interfaces.Generics.Base.IBaseService{TEntity}" />
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// The base repository
        /// </summary>
        private readonly IBaseRepository<TEntity> baseRepository;

        /// <summary>
        /// Gets or sets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        protected AbstractValidator<TEntity>? Validator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{TEntity}"/> class.
        /// </summary>
        /// <param name="baseRepository">The base repository.</param>
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual Task<bool> Create(TEntity entity)
        {
            this.Validate(entity);
            return this.baseRepository.Create(entity);
        }

        /// <summary>
        /// Deletes by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual Task<bool> Delete(uint id)
        {
            return this.baseRepository.Delete(id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> Read()
        {
            return this.baseRepository.Read();
        }

        /// <summary>
        /// Reads the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> Read(Expression<Func<TEntity, bool>> filter)
        {
            return this.baseRepository.Read(filter);
        }

        /// <summary>
        /// Reads by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual Task<TEntity?> Read(uint id)
        {
            return this.baseRepository.Read(id);
        }

        /// <summary>
        /// Reads with paged results.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="isAsc">if set to <c>true</c> [is asc].</param>
        /// <returns></returns>
        public virtual Task<Page<TEntity>> Read(uint pageIndex, uint pageSize, string? sortBy = null, bool isAsc = true)
        {
            return this.baseRepository.Read(pageIndex, pageSize, sortBy, isAsc);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual Task<bool> Update(TEntity entity)
        {
            this.Validate(entity);
            return this.baseRepository.Update(entity);
        }

        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="AppException"></exception>
        protected virtual void Validate(TEntity entity)
        {
            if (this.Validator != null)
            {
                var result = this.Validator.Validate(entity);
                if (!result.IsValid)
                {
                    throw new AppException(AppExceptionTypes.Validation, result.Errors.FirstOrDefault()?.ErrorMessage);
                }
            }
        }
    }
}
