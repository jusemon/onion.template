namespace Company.Project.UI.Controllers.Security
{
    using Application.Interfaces.Generics.Base;
    using Application.Interfaces.Security.DTOs;
    using AutoMapper;
    using Domain.Entities.Security;
    using Generics.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Menu Controller class. 
    /// </summary>
    /// <seealso cref="Generics.Base.BaseController{MenuDto, Menu}" />
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : BaseController<MenuDto, Menu>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityController"/> class.
        /// </summary>
        /// <param name="baseApplication">The base application.</param>
        /// <param name="mapper">The automapper instance.</param>
        public MenuController(IBaseApplication<Menu> baseApplication, IMapper mapper) : base(baseApplication, mapper)
        {

        }
    }
}
