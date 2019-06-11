namespace Company.Proyect.Domain.Entities.Security
{
    using Dapper.Contrib.Extensions;
    using Generics.Base;

    public class User : BaseEntity
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        [Computed]
        public string Token { get; set; }
    }
}
