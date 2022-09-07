namespace Company.Project.Domain.Entities.Security
{
    using Dapper.Contrib.Extensions;
    using Generics.Base;
    using System.Collections.Generic;

    /// <summary>
    /// Action class. 
    /// </summary>
    /// <seealso cref="Company.Project.Domain.Entities.Generics.Base.BaseEntity" />
    [Table("Actions")]
    public class Actions : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        [Computed]
        public virtual ICollection<Permissions>? Permissions { get; set; }
    }
}
