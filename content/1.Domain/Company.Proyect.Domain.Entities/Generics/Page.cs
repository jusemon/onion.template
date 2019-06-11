namespace Company.Proyect.Domain.Entities.Generics
{
    using Base;
    using System.Collections.Generic;

    public class Page<TEntity> where TEntity : BaseEntity
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public long TotalItems { get; set; }

        public IEnumerable<TEntity> Items { get; set; }
    }
}
