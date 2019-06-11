namespace Company.Proyect.Infra.Data.Generics.Base
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

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly IList<string> pluralEndInEs = new List<string> { "s", "sh", "ch", "x" };

        private readonly IDbFactory dbFactory;

        public BaseRepository(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

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

        private string GetTableName()
        {
            var tableName = typeof(TEntity).Name;
            tableName += this.pluralEndInEs.Any(p => tableName.EndsWith(p)) ? "es" : "s";
            return tableName;
        }

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
