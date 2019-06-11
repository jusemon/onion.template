namespace Company.Proyect.Application.Interfaces.Generics.Base
{
    using Domain.Entities.Generics;
    using Domain.Entities.Generics.Base;
    using System.Collections.Generic;

    public interface IBaseApplication<TEntity> where TEntity : BaseEntity, new()
    {
        Response<bool> Create(TEntity entity);

        Response<IEnumerable<TEntity>> Read();

        Response<TEntity> Read(int id);

        Response<Page<TEntity>> Read(int pageIndex, int pageSize, string sortBy = null, bool isAsc = true);

        Response<bool> Update(TEntity entity);

        Response<bool> Delete(int id);
    }
}
