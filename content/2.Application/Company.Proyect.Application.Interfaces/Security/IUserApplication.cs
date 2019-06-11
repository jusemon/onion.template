namespace Company.Proyect.Application.Interfaces.Security
{
    using Domain.Entities.Security;
    using Generics;
    using Generics.Base;

    public interface IUserApplication : IBaseApplication<User>
    {
        Response<User> Login(User user);

        Response<bool> SendRecovery(string email, string uri);

        Response<User> CheckRecoveryToken(User user);

        Response<User> UpdatePassword(User user, string uri);
    }
}
