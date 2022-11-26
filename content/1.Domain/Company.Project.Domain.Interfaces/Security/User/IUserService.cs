namespace Company.Project.Domain.Interfaces.Security.User
{
    using Entities.Security;
    using Generics.Base;

    /// <summary>
    /// User Service interface. 
    /// </summary>
    /// <seealso cref="Company.Project.Domain.Interfaces.Generics.Base.IBaseService{Company.Project.Domain.Entities.Security.User}" />
    public interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        Task<User> Login(string user, string password);

        /// <summary>
        /// Gets the user with recovery token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<User?> GetUserWithRecoveryToken(string email);

        /// <summary>
        /// Checks the recovery token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<User> CheckRecoveryToken(uint id, string token);
    }
}
