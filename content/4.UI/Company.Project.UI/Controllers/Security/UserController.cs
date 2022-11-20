namespace Company.Project.UI.Controllers.Security
{
    using Application.Interfaces.Generics.Base;
    using Application.Interfaces.Security.DTOs;
    using AutoMapper;
    using Domain.Entities.Security;
    using Generics.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// User Controller class. 
    /// </summary>
    /// <seealso cref="Generics.Base.BaseController{UserDto, User}" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<UserDto, User>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="baseApplication">The base application.</param>
        /// <param name="mapper">The automapper instance.</param>
        public UserController(IBaseApplication<User> baseApplication, IMapper mapper) : base(baseApplication, mapper)
        {

        }
    }
}
