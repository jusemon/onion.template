namespace Company.Proyect.UI.Controllers
{
    using Application.Interfaces.Generics;
    using Application.Interfaces.Security;
    using Domain.Entities.Security;
    using Generics.Base;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        private readonly IUserApplication userApplication;

        public UserController(IUserApplication userApplication) : base(userApplication)
        {
            this.userApplication = userApplication;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public virtual ActionResult<Response<User>> Login([FromBody] User entity)
        {
            return this.userApplication.Login(entity);
        }


        [AllowAnonymous]
        [HttpGet("SendRecovery")]
        public Response<bool> SendRecovery(string email)
        {

            var uri = new Uri(this.Request.GetDisplayUrl()).GetLeftPart(UriPartial.Authority);
            return this.userApplication.SendRecovery(email, uri);
        }

        [AllowAnonymous]
        [HttpPost("CheckRecoveryToken")]
        public Response<User> CheckRecoveryToken([FromBody] User user)
        {
            return this.userApplication.CheckRecoveryToken(user);
        }

        [AllowAnonymous]
        [HttpPost("UpdatePassword")]
        public Response<User> UpdatePassword([FromBody] User user)
        {
            var uri = new Uri(this.Request.GetDisplayUrl()).GetLeftPart(UriPartial.Authority);
            return this.userApplication.UpdatePassword(user, uri);
        }
    }
}
