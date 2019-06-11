namespace Company.Proyect.Domain.Interfaces.Security.User
{
    using Entities.Security;
    using Generics.Base;

    public interface IUserService : IBaseService<User>
    {
        void Login(User entity);

        User GetUserWithRecoveryToken(string email);

        User CheckRecoveryToken(User user);
    }
}
