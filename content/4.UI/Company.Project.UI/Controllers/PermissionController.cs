namespace Company.Project.UI.Controllers
{
    using Application.Interfaces.Generics.Base;
    using Domain.Entities.Security;
    using Generics.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Permission Controller class. 
    /// </summary>
    /// <seealso cref="Generics.Base.BaseController{Permission}" />
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseController<Permission>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionController"/> class.
        /// </summary>
        /// <param name="baseApplication">The base application.</param>
        public PermissionController(IBaseApplication<Permission> baseApplication) : base(baseApplication)
        {

        }
    }
}
