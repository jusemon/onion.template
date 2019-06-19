namespace Company.Project.UI.Controllers
{
    using Application.Interfaces.Generics;
    using Application.Interfaces.Security;
    using Domain.Entities.Security;
    using Generics.Base;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System;

    /// <summary>
    /// User Controller class. 
    /// </summary>
    /// <seealso cref="Generics.Base.BaseController{User}" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        /// <summary>
        /// The user application
        /// </summary>
        private readonly IUserApplication userApplication;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userApplication">The user application.</param>
        public UserController(IUserApplication userApplication) : base(userApplication)
        {
            this.userApplication = userApplication;
        }

        /// <summary>
        /// Logins the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public virtual ActionResult<Response<User>> Login([FromBody] User entity)
        {
            return this.userApplication.Login(entity);
        }


        /// <summary>
        /// Sends the recovery.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("SendRecovery")]
        public Response<bool> SendRecovery(string email)
        {

            var uri = new Uri(this.Request.GetDisplayUrl()).GetLeftPart(UriPartial.Authority);
            return this.userApplication.SendRecovery(email, uri);
        }

        /// <summary>
        /// Checks the recovery token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("CheckRecoveryToken")]
        public Response<User> CheckRecoveryToken([FromBody] User user)
        {
            return this.userApplication.CheckRecoveryToken(user);
        }

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("UpdatePassword")]
        public Response<User> UpdatePassword([FromBody] User user)
        {
            var uri = new Uri(this.Request.GetDisplayUrl()).GetLeftPart(UriPartial.Authority);
            return this.userApplication.UpdatePassword(user, uri);
        }
    }
}
