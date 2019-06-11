namespace Company.Proyect.Domain.Interfaces.Generics.Base
{
    using Entities.Generics;
    using Entities.Generics.Base;
    using System.Collections.Generic;

    public interface IBaseService<TEntity> where TEntity : BaseEntity, new()
    {
        bool Create(TEntity entity);

        IEnumerable<TEntity> Read();

        TEntity Read(int id);

        Page<TEntity> Read(int pageIndex, int pageSize, string sortBy = null, bool isAsc = true);

        bool Update(TEntity entity);

        bool Delete(int id);
    }
}
