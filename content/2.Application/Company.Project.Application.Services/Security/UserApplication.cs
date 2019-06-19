namespace Company.Project.Application.Services.Security
{
    using Domain.Entities.Security;
    using Domain.Interfaces.Email;
    using Domain.Interfaces.Security.User;
    using Generics.Base;
    using Infra.Utils.Objects;
    using Interfaces.Generics;
    using Interfaces.Security;
    using Microsoft.AspNetCore.WebUtilities;
    using System.IO;

    /// <summary>
    /// User Application class. 
    /// </summary>
    /// <seealso cref="Company.Project.Application.Services.Generics.Base.BaseApplication{Company.Project.Domain.Entities.Security.User}" />
    /// <seealso cref="Company.Project.Application.Interfaces.Security.IUserApplication" />
    public class UserApplication : BaseApplication<User>, IUserApplication
    {
        /// <summary>
        /// The base service
        /// </summary>
        private readonly IUserService baseService;

        /// <summary>
        /// The email service
        /// </summary>
        private readonly IEmailService emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserApplication"/> class.
        /// </summary>
        /// <param name="baseService">The base service.</param>
        /// <param name="emailService">The email service.</param>
        public UserApplication(IUserService baseService, IEmailService emailService) : base(baseService)
        {
            this.baseService = baseService;
            this.emailService = emailService;
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Response<User> Login(User user)
        {
            return this.Try(() =>
            {
                this.baseService.Login(user);
                return user;
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
            return this.Try(() =>
            {
                var user = this.baseService.GetUserWithRecoveryToken(email);
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
        public Response<User> CheckRecoveryToken(User user)
        {
            return this.Try(() => this.baseService.CheckRecoveryToken(user));
        }

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public Response<User> UpdatePassword(User user, string uri)
        {
            return this.Try(() =>
            {
                var currentUser = this.baseService.CheckRecoveryToken(user);
                currentUser.Password = user.Password;
                this.baseService.Update(currentUser);
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
