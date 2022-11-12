namespace Company.Project.UI.Controllers
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
    public class ActivityController : BaseController<Activity>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityController"/> class.
        /// </summary>
        /// <param name="baseApplication">The base application.</param>
        public ActivityController(IBaseApplication<Activity> baseApplication) : base(baseApplication)
        {

        }
    }
}
