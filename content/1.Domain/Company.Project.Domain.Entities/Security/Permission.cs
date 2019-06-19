namespace Company.Project.Domain.Entities.Security
{
    using Generics.Base;

    /// <summary>
    /// Permission class. 
    /// </summary>
    /// <seealso cref="Company.Project.Domain.Entities.Generics.Base.BaseEntity" />
    public class Permission : BaseEntity
    {
        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public long RoleId { get; set; }

        /// <summary>
        /// Gets or sets the action identifier.
        /// </summary>
        /// <value>
        /// The action identifier.
        /// </value>
        public long ActionId { get; set; }
    }
}
