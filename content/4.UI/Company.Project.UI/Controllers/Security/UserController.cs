namespace Company.Project.UI.Controllers.Security
{
    using Application.Interfaces.Generics.Base;
    using Domain.Entities.Security;
    using Generics.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// User Controller class. 
    /// </summary>
    /// <seealso cref="Generics.Base.BaseController{User}" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<Users>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="baseApplication">The base application.</param>
        public UserController(IBaseApplication<Users> baseApplication) : base(baseApplication)
        {

        }
    }
}
