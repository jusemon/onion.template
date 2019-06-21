namespace Company.Project.Infra.Data.Generics.Base
{
    using Contexts;
    using Domain.Entities.Generics;
    using Domain.Entities.Generics.Base;
    using Domain.Interfaces.Generics.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utils.Exceptions;

    /// <summary>
    /// Base Repository class. 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Company.Project.Domain.Interfaces.Generics.Base.IBaseRepository{TEntity}" />
    public class EFSQLiteBaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// The security context
        /// </summary>
        private readonly SecurityContext securityContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteBaseRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbFactory">The database factory.</param>
        public EFSQLiteBaseRepository(SecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual bool Create(TEntity entity)
        {
            return this.Try(() =>
            {
                var con = this.securityContext;
                entity.CreatedAt = DateTime.Now;
                con.Add(entity);
                con.SaveChanges();
                entity.Id = entity.Id;
                return true;

            });
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Read()
        {
            return this.Try(() =>
            {
                var con = this.securityContext;
                return con.Set<TEntity>().AsNoTracking().ToList();
            });
        }

        /// <summary>
        /// Reads by the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Read(Func<TEntity, bool> filter)
        {
            return this.Try(() =>
            {
                var con = this.securityContext;
                var list = con.Set<TEntity>().AsNoTracking();
                list = list.Where(e => filter(e));
                return list.ToList();
            });
        }

        /// <summary>
        /// Reads by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual TEntity Read(long id)
        {
            return this.Try(() =>
            {
                var con = this.securityContext;
                return con.Set<TEntity>().AsNoTracking().FirstOrDefault(e => e.Id == id);
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
        public virtual Page<TEntity> Read(int pageIndex, int pageSize, string sortBy = null, bool isAsc = true)
        {
            return this.Try(() =>
            {
                var con = this.securityContext;
                var totalItems = con.Set<TEntity>().AsNoTracking().LongCount();
                var offset = pageSize * (pageIndex - 1);
                var query = con.Set<TEntity>().SortBy(sortBy, isAsc).Skip(offset).Take(pageSize)
                    .Include(e => e.CreatedByUser)
                    .Include(e => e.LastUpdatedByUser);
                return new Page<TEntity>
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalItems = totalItems,
                    Items = query.AsNoTracking().ToList().Select(s =>
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
        public virtual bool Update(TEntity entity)
        {
            return this.Try(() =>
            {
                var con = this.securityContext;
                entity.LastUpdatedAt = DateTime.Now;
                con.Update(entity);
                con.SaveChanges();
                return true;
            });
        }

        /// <summary>
        /// Deletes by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual bool Delete(int id)
        {
            return this.Try(() =>
            {
                var con = this.securityContext;
                TEntity entity = new TEntity { Id = id };
                con.Remove(entity);
                con.SaveChanges();
                return true;
            });
        }

        /// <summary>
        /// Deletes the specified ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public virtual bool Delete(IEnumerable<int> ids)
        {
            return this.Try(() =>
            {
                var con = this.securityContext;
                con.RemoveRange(ids.Select(i => new TEntity { Id = i }));
                con.SaveChanges();
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

    public static class Extensions
    {
        public static IOrderedQueryable<TEntity> SortBy<TEntity>(this IQueryable<TEntity> q, string sortBy, bool isAsc) where TEntity : BaseEntity
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                var atts = typeof(TEntity).GetProperties().FirstOrDefault(p => p.Name.Equals(sortBy, StringComparison.InvariantCultureIgnoreCase));
                if (atts != null)
                {
                    return isAsc ? q.OrderBy(e => atts.GetValue(e)) : q.OrderByDescending(e => atts.GetValue(e));
                }
            }
            return q.OrderBy(e => e.Id);
        }
    }
}
