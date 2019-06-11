namespace Company.Proyect.UI.Controllers
{
    using Application.Interfaces.Generics.Base;
    using Domain.Entities.Security;
    using Generics.Base;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController<Role>
    {

        public RoleController(IBaseApplication<Role> baseApplication) : base(baseApplication)
        {

        }
    }
}
