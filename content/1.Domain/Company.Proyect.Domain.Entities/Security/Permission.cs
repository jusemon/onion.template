namespace Company.Proyect.Domain.Entities.Security
{
    using Generics.Base;

    public class Permission : BaseEntity
    {
        public long RoleId { get; set; }

        public long ActionId { get; set; }
    }
}
