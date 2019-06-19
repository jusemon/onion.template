namespace Company.Project.Infra.Data.Generics.Base
{
    using Dapper;
    using Dapper.Contrib.Extensions;
    using Domain.Entities.Generics;
    using Domain.Entities.Generics.Base;
    using Domain.Interfaces.Data;
    using Domain.Interfaces.Generics.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utils.Exceptions;

    /// <summary>
    /// Base Repository class. 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Company.Project.Domain.Interfaces.Generics.Base.IBaseRepository{TEntity}" />
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// The plural end in es
        /// </summary>
        private readonly IList<string> pluralEndInEs = new List<string> { "s", "sh", "ch", "x" };

        /// <summary>
        /// The database factory
        /// </summary>
        private readonly IDbFactory dbFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbFactory">The database factory.</param>
        public BaseRepository(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
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
                using (var con = this.dbFactory.GetConnection())
                {
                    entity.CreatedAt = DateTime.Now;
                    long id = con.Insert<TEntity>(entity);
                    entity.Id = id;
                    return true;
                }
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
                using (var con = this.dbFactory.GetConnection())
                {
                    return con.GetAll<TEntity>();
                }
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
                using (var con = this.dbFactory.GetConnection())
                {
                    var list = con.GetAll<TEntity>();
                    return list.Where(e => filter(e));
                }
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
                using (var con = this.dbFactory.GetConnection())
                {
                    return con.Get<TEntity>(id);
                }
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
                using (var con = this.dbFactory.GetConnection())
                {
                    string tableName = this.GetTableName();
                    var offset = pageSize * (pageIndex - 1);
                    var totalItems = con.Query<long>($"SELECT COUNT(*) FROM {tableName}").FirstOrDefault();
                    var sortByQuery = string.Empty;
                    if (sortBy != null)
                    {
                        sortByQuery = $"ORDER BY @sortBy {(!isAsc ? "DESC" : "")}";
                    }
                    var list = con.Query<TEntity>($"SELECT * FROM {tableName} {sortByQuery} LIMIT @pageSize OFFSET @offset", new { pageSize, offset, sortBy });
                    return new Page<TEntity>
                    {
                        PageIndex = pageIndex,
                        PageSize = pageSize,
                        TotalItems = totalItems,
                        Items = list
                    };
                }
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
                using (var con = this.dbFactory.GetConnection())
                {
                    entity.LastUpdatedAt = DateTime.Now;
                    return con.Update<TEntity>(entity);
                }
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
                using (var con = this.dbFactory.GetConnection())
                {
                    TEntity entity = new TEntity { Id = id };
                    return con.Delete<TEntity>(entity);
                }
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
                using (var con = this.dbFactory.GetConnection())
                {
                    var tableName = this.GetTableName();
                    con.Query($"DELETE FROM {tableName} WHERE Id in @ids", new { ids });
                    return true;
                }
            });
        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <returns></returns>
        private string GetTableName()
        {
            var tableName = typeof(TEntity).Name;
            tableName += this.pluralEndInEs.Any(p => tableName.EndsWith(p)) ? "es" : "s";
            return tableName;
        }

        /// <summary>
        /// Tries the specified action.
        /// </summary>
        /// <typeparam name="TOut">The type of the out.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        private TOut Try<TOut>(Func<TOut> action)
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
}
