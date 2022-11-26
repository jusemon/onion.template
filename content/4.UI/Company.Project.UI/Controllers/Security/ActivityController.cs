namespace Company.Project.UI.Controllers
{
    using Application.Interfaces.Generics.Base;
    using Application.Interfaces.Security.DTOs;
    using AutoMapper;
    using Domain.Entities.Security;
    using Generics.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Activity Controller class. 
    /// </summary>
    /// <seealso cref="Generics.Base.BaseController{ActivityDTO, Action}" />
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : BaseController<ActivityDTO, Activity>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityController"/> class.
        /// </summary>
        /// <param name="baseApplication">The base application.</param>
        /// <param name="mapper">The automapper instance.</param>
        public ActivityController(IBaseApplication<Activity> baseApplication, IMapper mapper) : base(baseApplication, mapper)
        {

        }
    }
}
