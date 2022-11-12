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
        public virtual async Task<ActionResult<UserLoginToken>> Login([FromBody] UserLogin entity)
        {
            var result = await this.authApplication.Login(entity);
            return GetResponse(result);
        }


        /// <summary>
        /// Sends the recovery.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("SendRecovery")]
        public async Task<ActionResult<bool>> SendRecovery(string email)
        {

            var uri = new Uri(this.Request.GetDisplayUrl()).GetLeftPart(UriPartial.Authority);
            var result = await this.authApplication.SendRecovery(email, uri);
            return GetResponse(result);
        }

        /// <summary>
        /// Checks the recovery token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("CheckRecoveryToken")]
        public async Task<ActionResult<User>> CheckRecoveryToken([FromBody] UserLoginToken user)
        {
            var result = await this.authApplication.CheckRecoveryToken(user);
            return GetResponse(result);
        }

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("UpdatePassword")]
        public async Task<ActionResult<User>> UpdatePassword([FromBody] User user)
        {
            var uri = new Uri(this.Request.GetDisplayUrl()).GetLeftPart(UriPartial.Authority);
            var result = await this.authApplication.UpdatePassword(user, uri);
            return GetResponse(result);
        }

        /// <summary>
        /// Get the result from the response when is success otherwise a BadRequest
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        protected ActionResult<TResult> GetResponse<TResult>(Response<TResult> response)
        {
            if (response.IsSuccess)
            {
                return response.Result!;
            }

            if (response.ExceptionType == Infra.Utils.Exceptions.AppExceptionTypes.Database && response.ExceptionMessage!.Contains("Not found"))
            {
                return NotFound(new { response.ExceptionType, response.ExceptionMessage });
            }

            return BadRequest(new { response.ExceptionType, response.ExceptionMessage });
        }
    }
}
