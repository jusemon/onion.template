namespace Company.Project.UI.Controllers.Generics.Base
{
    using Application.Interfaces.Generics;
    using Application.Interfaces.Generics.Base;
    using AutoMapper;
    using Domain.Entities.Generics;
    using Domain.Entities.Generics.Base;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using UI.ValidateClaim;

    /// <summary>
    /// Base Controller class. 
    /// </summary>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class BaseController<TDto, TEntity> : ControllerBase
        where TDto : new()
        where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// The base application
        /// </summary>
        private readonly IBaseApplication<TEntity> baseApplication;

        /// <summary>
        /// The automapper instance
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController{TDto, TEntity}"/> class.
        /// </summary>
        /// <param name="baseApplication">The base application.</param>
        /// <param name="mapper">The automapper instance.</param>
        public BaseController(IBaseApplication<TEntity> baseApplication, IMapper mapper)
        {
            this.baseApplication = baseApplication;
            this.mapper = mapper;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateClaim("[controller].create")]
        public virtual async Task<ActionResult<bool>> Create([FromBody] TDto dto)
        {
            var entity = this.mapper.Map<TEntity>(dto);
            entity.CreatedBy = Convert.ToUInt32(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await this.baseApplication.Create(entity);
            return GetResponse(response);
        }

        /// <summary>
        /// Deletes by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ValidateClaim("[controller].delete")]
        public virtual async Task<ActionResult<bool>> Delete(uint id)
        {
            var response = await this.baseApplication.Delete(id);
            return GetResponse(response);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ValidateClaim("[controller].read")]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> Read()
        {
            var response = await this.baseApplication.Read();
            return GetResponse(response);
        }

        /// <summary>
        /// Reads by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ValidateClaim("[controller].read")]
        public virtual async Task<ActionResult<TEntity?>> Read(uint id)
        {
            var response = await this.baseApplication.Read(id);
            return GetResponse(response);
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
        public virtual async Task<ActionResult<Page<TEntity>>> Read(uint pageIndex, uint pageSize, string? sortBy = null, bool isAsc = true)
        {
            var response = await this.baseApplication.Read(pageIndex, pageSize, sortBy, isAsc);
            return GetResponse(response);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        /// <param name="dto">The entity.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateClaim("[controller].update")]
        public virtual async Task<ActionResult<bool>> Update(uint id, [FromBody] TDto dto)
        {
            var entity = this.mapper.Map<TEntity>(dto);
            entity.Id = id;
            entity.LastUpdatedBy = Convert.ToUInt32(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await this.baseApplication.Update(entity);
            return GetResponse(response);
        }

        /// <summary>
        /// Get the result from the response when is success otherwise a BadRequest
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        protected ActionResult<TResult> GetResponse<TResult>(Response<TResult> response)
        {
            if (response.IsSuccess)
            {
                return response.Result!;
            }

            if (response.ExceptionType == Infra.Utils.Exceptions.AppExceptionTypes.Database && response.ExceptionMessage!.Contains("Not found"))
            {
                return NotFound(new { response.ExceptionType, response.ExceptionMessage });
            }

            return BadRequest(new { response.ExceptionType, response.ExceptionMessage });
        }
    }
}
