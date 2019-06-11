namespace Company.Proyect.Domain.Entities.Security
{
    using Generics.Base;

    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public bool IsAdmin { get; set; }
    }
}
