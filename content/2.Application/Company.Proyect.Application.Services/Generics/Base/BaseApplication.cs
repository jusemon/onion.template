namespace Company.Proyect.Application.Services.Generics.Base
{
    using Interfaces.Generics;
    using Interfaces.Generics.Base;
    using Domain.Entities.Generics;
    using Domain.Entities.Generics.Base;
    using Domain.Interfaces.Generics.Base;
    using System.Collections.Generic;

    public class BaseApplication<TEntity> : IBaseApplication<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly IBaseService<TEntity> baseService;

        public BaseApplication(IBaseService<TEntity> baseService)
        {
            this.baseService = baseService;
        }

        public virtual Response<bool> Create(TEntity entity)
        {
            return this.Try(() => this.baseService.Create(entity));
        }

        public virtual Response<bool> Delete(int id)
        {
            return this.Try(() => this.baseService.Delete(id));
        }

        public virtual Response<IEnumerable<TEntity>> Read()
        {
            return this.Try(() => this.baseService.Read());
        }

        public virtual Response<TEntity> Read(int id)
        {
            return this.Try(() => this.baseService.Read(id));
        }

        public virtual Response<Page<TEntity>> Read(int pageIndex, int pageSize, string sortBy = null, bool isAsc = true)
        {
            return this.Try(() => this.baseService.Read(pageIndex, pageSize, sortBy, isAsc));
        }

        public virtual Response<bool> Update(TEntity entity)
        {
            return this.Try(() => this.baseService.Update(entity));
        }
    }
}
