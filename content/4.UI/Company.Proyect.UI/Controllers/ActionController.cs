namespace Company.Proyect.UI.Controllers
{
    using Application.Interfaces.Generics.Base;
    using Domain.Entities.Security;
    using Generics.Base;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : BaseController<Action>
    {

        public ActionController(IBaseApplication<Action> baseApplication) : base(baseApplication)
        {

        }
    }
}
