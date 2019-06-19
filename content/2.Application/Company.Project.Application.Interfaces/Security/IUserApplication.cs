namespace Company.Project.Application.Interfaces.Security
{
    using Domain.Entities.Security;
    using Generics;
    using Generics.Base;

    /// <summary>
    /// User Application interface. 
    /// </summary>
    /// <seealso cref="Company.Project.Application.Interfaces.Generics.Base.IBaseApplication{Company.Project.Domain.Entities.Security.User}" />
    public interface IUserApplication : IBaseApplication<User>
    {
        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Response<User> Login(User user);

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
        Response<User> CheckRecoveryToken(User user);

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        Response<User> UpdatePassword(User user, string uri);
    }
}
