namespace Company.Project.Application.Services.Security
{
    using Domain.Entities.Security;
    using Domain.Interfaces.Email;
    using Domain.Interfaces.Security.User;
    using Infra.Utils.Objects;
    using Interfaces.Generics;
    using Interfaces.Security;
    using Interfaces.Security.DTOs;
    using Microsoft.AspNetCore.WebUtilities;
    using System.IO;

    /// <summary>
    /// User Application class.
    /// </summary>
    /// <seealso cref="Company.Project.Application.Interfaces.Security.IAuthApplication" />
    public class AuthApplication : IAuthApplication
    {
        /// <summary>
        /// The base service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The email service
        /// </summary>
        private readonly IEmailService emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthApplication" /> class.
        /// </summary>
        /// <param name="userService">The base service.</param>
        /// <param name="emailService">The email service.</param>
        public AuthApplication(IUserService userService, IEmailService emailService)
        {
            this.userService = userService;
            this.emailService = emailService;
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Response<UserLoginToken> Login(UserLogin user)
        {
            return ApplicationExtensions.Try(() =>
            {
                var result = this.userService.Login(user.Username, user.Password);
                return new UserLoginToken {
                    Id = result.Id,
                    Username = result.Username,
                    Email = result.Email,
                    Token = result.Token
                };
            });
        }

        /// <summary>
        /// Sends the recovery email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public Response<bool> SendRecovery(string email, string uri)
        {
            return ApplicationExtensions.Try(() =>
            {
                var user = this.userService.GetUserWithRecoveryToken(email);
                if (user == null)
                {
                    return false;
                }

                var template = File.ReadAllText("Templates/EmailRecovery.cshtml");
                var urlToken = QueryHelpers.AddQueryString($"{uri}/{{0}}", new { token = user?.Token, id = user?.Id.ToString() }.AsDictionary<string>());
                emailService.Send(user.Email, "Recuperar Contraseña", template, new { Name = user.Username, UrlBase = uri, UrlToken = urlToken }.ToDynamic(), true);
                return true;
            });
        }

        /// <summary>
        /// Checks the recovery token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Response<Users> CheckRecoveryToken(UserLoginToken user)
        {
            return ApplicationExtensions.Try(() => 
                this.userService.CheckRecoveryToken(user.Id, user.Token)
            );
        }

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public Response<Users> UpdatePassword(Users user, string uri)
        {
            return ApplicationExtensions.Try(() =>
            {
                var currentUser = this.userService.CheckRecoveryToken(user.Id, user.Token);
                currentUser.Password = user.Password;
                this.userService.Update(currentUser);
                if (currentUser != null)
                {
                    var template = File.ReadAllText("Templates/PasswordChanged.cshtml");
                    emailService.Send(user.Email, "Tu contraseña de AppTitle ha cambiado", template, new { Name = user.Username, UrlBase = uri }.ToDynamic(), true);
                }
                return currentUser;
            });
        }
    }
}
