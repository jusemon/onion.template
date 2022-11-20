namespace Company.Project.Domain.Entities.Generics
{
    using Base;
    using System.Collections.Generic;

    /// <summary>
    /// Page class. 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class Page<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>
        /// The index of the page.
        /// </value>
        public uint PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public uint PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total items.
        /// </summary>
        /// <value>
        /// The total items.
        /// </value>
        public uint TotalItems { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IEnumerable<TEntity> Items { get; set; } = null!;
    }
}
