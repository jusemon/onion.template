namespace Company.Project.UI.Controllers.Security
{
    using Application.Interfaces.Generics;
    using Application.Interfaces.Security;
    using Company.Project.Application.Interfaces.Security.DTOs;
    using Domain.Entities.Security;
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
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// The user application
        /// </summary>
        private readonly IAuthApplication authApplication;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="authApplication">The user application.</param>
        public AuthController(IAuthApplication authApplication)
        {
            this.authApplication = authApplication;
        }

        /// <summary>
        /// Logins the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public virtual ActionResult<Response<UserLoginToken>> Login([FromBody] UserLogin entity)
        {
            return this.authApplication.Login(entity);
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
            return this.authApplication.SendRecovery(email, uri);
        }

        /// <summary>
        /// Checks the recovery token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("CheckRecoveryToken")]
        public Response<Users> CheckRecoveryToken([FromBody] UserLoginToken user)
        {
            return this.authApplication.CheckRecoveryToken(user);
        }

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("UpdatePassword")]
        public Response<Users> UpdatePassword([FromBody] Users user)
        {
            var uri = new Uri(this.Request.GetDisplayUrl()).GetLeftPart(UriPartial.Authority);
            return this.authApplication.UpdatePassword(user, uri);
        }
    }
}
