namespace Company.Project.UI.Controllers.Security
{
    using Application.Interfaces.Generics.Base;
    using Application.Interfaces.Security.DTOs;
    using AutoMapper;
    using Domain.Entities.Security;
    using Generics.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Role Controller class. 
    /// </summary>
    /// <seealso cref="Generics.Base.BaseController{RoleDto, Role}" />
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController<RoleDto, Role>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="baseApplication">The base application.</param>
        /// <param name="mapper">The automapper instance.</param>
        public RoleController(IBaseApplication<Role> baseApplication, IMapper mapper) : base(baseApplication, mapper)
        {

        }
    }
}
