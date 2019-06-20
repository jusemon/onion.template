namespace Company.Project.UI.Controllers
{
    using Application.Interfaces.Generics.Base;
    using Domain.Entities.Security;
    using Generics.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Role Controller class. 
    /// </summary>
    /// <seealso cref="Generics.Base.BaseController{Role}" />
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController<Roles>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="baseApplication">The base application.</param>
        public RoleController(IBaseApplication<Roles> baseApplication) : base(baseApplication)
        {

        }
    }
}
