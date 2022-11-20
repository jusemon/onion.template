namespace Company.Project.UI.Controllers.Security
{
    using Application.Interfaces.Generics.Base;
    using Application.Interfaces.Security.DTOs;
    using AutoMapper;
    using Domain.Entities.Security;
    using Generics.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Permission Controller class. 
    /// </summary>
    /// <seealso cref="Generics.Base.BaseController{PermissionDto, Permission}" />
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseController<PermissionDto, Permission>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionController"/> class.
        /// </summary>
        /// <param name="baseApplication">The base application.</param>
        /// <param name="mapper">The automapper instance.</param>
        public PermissionController(IBaseApplication<Permission> baseApplication, IMapper mapper) : base(baseApplication, mapper)
        {

        }
    }
}
