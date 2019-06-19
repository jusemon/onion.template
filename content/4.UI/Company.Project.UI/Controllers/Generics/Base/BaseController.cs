namespace Company.Project.UI.Controllers.Generics.Base
{
    using Application.Interfaces.Generics;
    using Application.Interfaces.Generics.Base;
    using Domain.Entities.Generics;
    using Domain.Entities.Generics.Base;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using UI.ValidateClaim;

    /// <summary>
    /// Base Controller class. 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class BaseController<TEntity> : ControllerBase where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// The base application
        /// </summary>
        private readonly IBaseApplication<TEntity> baseApplication;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController{TEntity}"/> class.
        /// </summary>
        /// <param name="baseApplication">The base application.</param>
        public BaseController(IBaseApplication<TEntity> baseApplication)
        {
            this.baseApplication = baseApplication;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateClaim("[controller].create")]
        public virtual ActionResult<Response<bool>> Create([FromBody] TEntity entity)
        {
            return this.baseApplication.Create(entity);
        }

        /// <summary>
        /// Deletes by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ValidateClaim("[controller].delete")]
        public virtual ActionResult<Response<bool>> Delete(int id)
        {
            return this.baseApplication.Delete(id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ValidateClaim("[controller].read")]
        public virtual ActionResult<Response<IEnumerable<TEntity>>> Read()
        {
            return this.baseApplication.Read();
        }

        /// <summary>
        /// Reads by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ValidateClaim("[controller].read")]
        public virtual ActionResult<Response<TEntity>> Read(int id)
        {
            return this.baseApplication.Read(id);
        }

        /// <summary>
        /// Reads with paged results.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="isAsc">if set to <c>true</c> [is asc].</param>
        /// <returns></returns>
        [HttpGet("Paged")]
        [ValidateClaim("[controller].read")]
        public virtual ActionResult<Response<Page<TEntity>>> Read(int pageIndex, int pageSize, string sortBy = null, bool isAsc = true)
        {
            return this.baseApplication.Read(pageIndex, pageSize, sortBy, isAsc);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut]
        [ValidateClaim("[controller].update")]
        public virtual ActionResult<Response<bool>> Update([FromBody] TEntity entity)
        {
            return this.baseApplication.Update(entity);
        }
    }
}
