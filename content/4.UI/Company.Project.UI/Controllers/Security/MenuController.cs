namespace Company.Project.UI.Controllers.Security
{
    using Application.Interfaces.Generics.Base;
    using Domain.Entities.Security;
    using Generics.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Action Controller class. 
    /// </summary>
    /// <seealso cref="Generics.Base.BaseController{Action}" />
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : BaseController<Menus>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionController"/> class.
        /// </summary>
        /// <param name="baseApplication">The base application.</param>
        public MenuController(IBaseApplication<Menus> baseApplication) : base(baseApplication)
        {

        }
    }
}
