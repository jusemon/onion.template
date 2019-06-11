namespace Company.Proyect.UI.Controllers.Generics.Base
{
    using Application.Interfaces.Generics;
    using Application.Interfaces.Generics.Base;
    using Domain.Entities.Generics;
    using Domain.Entities.Generics.Base;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using UI.ValidateClaim;

    public class BaseController<TEntity> : ControllerBase where TEntity : BaseEntity, new()
    {
        private readonly IBaseApplication<TEntity> baseApplication;

        public BaseController(IBaseApplication<TEntity> baseApplication)
        {
            this.baseApplication = baseApplication;
        }

        [HttpPost]
        [ValidateClaim("[controller].create")]
        public virtual ActionResult<Response<bool>> Create([FromBody] TEntity entity)
        {
            return this.baseApplication.Create(entity);
        }

        [HttpDelete("{id}")]
        [ValidateClaim("[controller].delete")]
        public virtual ActionResult<Response<bool>> Delete(int id)
        {
            return this.baseApplication.Delete(id);
        }

        [HttpGet]
        [ValidateClaim("[controller].read")]
        public virtual ActionResult<Response<IEnumerable<TEntity>>> Read()
        {
            return this.baseApplication.Read();
        }

        [HttpGet("{id}")]
        [ValidateClaim("[controller].read")]
        public virtual ActionResult<Response<TEntity>> Read(int id)
        {
            return this.baseApplication.Read(id);
        }

        [HttpGet("Paged")]
        [ValidateClaim("[controller].read")]
        public virtual ActionResult<Response<Page<TEntity>>> Read(int pageIndex, int pageSize, string sortBy = null, bool isAsc = true)
        {
            return this.baseApplication.Read(pageIndex, pageSize, sortBy, isAsc);
        }

        [HttpPut]
        [ValidateClaim("[controller].update")]
        public virtual ActionResult<Response<bool>> Update([FromBody] TEntity entity)
        {
            return this.baseApplication.Update(entity);
        }
    }
}
