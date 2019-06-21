namespace Company.Project.Application.Interfaces.Security
{
    using Interfaces.Security.DTOs;
    using Domain.Entities.Security;
    using Generics;

    /// <summary>
    /// User Application interface. 
    /// </summary>
    /// <seealso cref="Company.Project.Application.Interfaces.Generics.Base.IBaseApplication{Company.Project.Domain.Entities.Security.Users}" />
    public interface IAuthApplication
    {
        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Response<UserLoginToken> Login(UserLogin user);

        /// <summary>
        /// Sends the recovery email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        Response<bool> SendRecovery(string email, string uri);

        /// <summary>
        /// Checks the recovery token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Response<Users> CheckRecoveryToken(Users user);

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        Response<Users> UpdatePassword(Users user, string uri);
    }
}
