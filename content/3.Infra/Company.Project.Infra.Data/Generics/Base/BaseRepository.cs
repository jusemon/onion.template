namespace Company.Project.Infra.Data.Generics.Base
{
    using Contexts;
    using Domain.Entities.Generics;
    using Domain.Entities.Generics.Base;
    using Domain.Interfaces.Generics.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using Utils.Exceptions;

    /// <summary>
    /// Base Repository class. 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Company.Project.Domain.Interfaces.Generics.Base.IBaseRepository{TEntity}" />
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// The security context
        /// </summary>
        private readonly SecurityContext securityContext;

        /// <summary>
        /// The props
        /// </summary>
        private readonly PropertyDescriptorCollection props;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteBaseRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbFactory">The database factory.</param>
        public BaseRepository(SecurityContext securityContext)
        {
            this.securityContext = securityContext;
            this.props = TypeDescriptor.GetProperties(typeof(TEntity));
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual Task<bool> Create(TEntity entity)
        {
            return this.Try(async () =>
            {
                var con = this.securityContext;
                entity.CreatedAt = DateTime.UtcNow;
                con.Add(entity);
                await con.SaveChangesAsync();
                entity.Id = entity.Id;
                return true;

            });
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> Read()
        {
            return this.Try(async () =>
            {
                var con = this.securityContext;
                var items = await con.Set<TEntity>().AsNoTracking().ToListAsync();
                return items.AsEnumerable();
            });
        }

        /// <summary>
        /// Reads by the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>c
        public virtual Task<IEnumerable<TEntity>> Read(Expression<Func<TEntity, bool>> filter)
        {
            return this.Try(async () =>
            {
                var con = this.securityContext;
                var query = con.Set<TEntity>().Where(filter).AsNoTracking();
                var items = await query.ToListAsync();
                return items.AsEnumerable();
            });
        }

        /// <summary>
        /// Reads by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual Task<TEntity?> Read(ulong id)
        {
            return this.Try(async () =>
            {
                var con = this.securityContext;
                return await con.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            });
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
            return this.Try(async () =>
            {
                var con = this.securityContext;
                var offset = pageSize * pageIndex;
                var totalItems = (ulong)await con.Set<TEntity>().AsNoTracking().LongCountAsync();
                var items = await con.Set<TEntity>().SortBy(this.props, sortBy, isAsc).Skip((int)offset).Take((int)pageSize)
                    .Include(e => e.CreatedByUser)
                    .Include(e => e.LastUpdatedByUser).AsNoTracking().ToListAsync();
                return new Page<TEntity>
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalItems = totalItems,
                    Items = items.Select(s =>
                    {
                        if (s.CreatedByUser != null) s.CreatedByUser.Password = null;
                        if (s.LastUpdatedByUser != null) s.LastUpdatedByUser.Password = null;
                        return s;
                    })
                };
            });
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual Task<bool> Update(TEntity entity)
        {
            return this.Try(async () =>
            {
                var con = this.securityContext;
                entity.LastUpdatedAt = DateTime.UtcNow;
                con.Update(entity);
                await con.SaveChangesAsync();
                return true;
            });
        }

        /// <summary>
        /// Deletes by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual Task<bool> Delete(ulong id)
        {
            return this.Try(async () =>
            {
                var con = this.securityContext;
                TEntity entity = new TEntity { Id = id };
                con.Remove(entity);
                await con.SaveChangesAsync();
                return true;
            });
        }

        /// <summary>
        /// Deletes the specified ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public virtual Task<bool> Delete(IEnumerable<ulong> ids)
        {
            return this.Try(async () =>
            {
                var con = this.securityContext;
                con.RemoveRange(ids.Select(i => new TEntity { Id = i }));
                await con.SaveChangesAsync();
                return true;
            });
        }

        /// <summary>
        /// Tries the specified action.
        /// </summary>
        /// <typeparam name="TOut">The type of the out.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        protected TOut Try<TOut>(Func<TOut> action)
        {
            try
            {
                return action();
            }
            catch (System.Exception e)
            {
                throw new AppException(AppExceptionTypes.Database, e.Message, e);
            }
        }
    }

    /// <summary>
    /// Extensions class. 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Sorts the by.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="q">The q.</param>
        /// <param name="props">The props.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="isAsc">if set to <c>true</c> [is asc].</param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> SortBy<TEntity>(this IQueryable<TEntity> q, PropertyDescriptorCollection props, string? sortBy, bool isAsc) where TEntity : BaseEntity
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                var prop = props.Find(sortBy, true);
                if (prop != null)
                {
                    return isAsc ? q.OrderBy(e => prop.GetValue(e)) : q.OrderByDescending(e => prop.GetValue(e));
                }
            }
            return q.OrderBy(e => e.Id);
        }
    }
}
