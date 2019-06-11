namespace Company.Proyect.Application.Services.Security
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

    public class UserApplication : BaseApplication<User>, IUserApplication
    {
        private readonly IUserService baseService;

        private readonly IEmailService emailService;

        public UserApplication(IUserService baseService, IEmailService emailService) : base(baseService)
        {
            this.baseService = baseService;
            this.emailService = emailService;
        }

        public Response<User> Login(User user)
        {
            return this.Try(() =>
            {
                this.baseService.Login(user);
                return user;
            });
        }

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

        public Response<User> CheckRecoveryToken(User user)
        {
            return this.Try(() => this.baseService.CheckRecoveryToken(user));
        }

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
                    emailService.Send(user.Email, "Tu contraseña de Préstame ha cambiado", template, new { Name = user.Username, UrlBase = uri }.ToDynamic(), true);
                }
                return currentUser;
            });
        }
    }
}
