namespace Company.Proyect.Domain.Entities.Generics.Base
{
    using System;

    public class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? LastUpdatedAt { get; set; }

        public int? LastUpdatedBy { get; set; }
    }
}
