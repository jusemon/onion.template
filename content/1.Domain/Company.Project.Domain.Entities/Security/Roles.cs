namespace Company.Project.Domain.Entities.Security
{
    using Dapper.Contrib.Extensions;
    using Generics.Base;
    using System.Collections.Generic;

    /// <summary>
    /// Role class. 
    /// </summary>
    /// <seealso cref="Company.Project.Domain.Entities.Generics.Base.BaseEntity" />
    [Table("Roles")]
    public class Roles : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is admin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        [Computed]
        public virtual ICollection<Permissions>? Permissions { get; set; }

        [Computed]
        public virtual ICollection<Users>? Users { get; set; }
    }
}
