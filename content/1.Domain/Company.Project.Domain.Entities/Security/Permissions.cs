namespace Company.Project.Domain.Entities.Security
{
    using Dapper.Contrib.Extensions;
    using Generics.Base;

    /// <summary>
    /// Permission class. 
    /// </summary>
    /// <seealso cref="Company.Project.Domain.Entities.Generics.Base.BaseEntity" />
    [Table("Permissions")]
    public class Permissions : BaseEntity
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

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        [Computed]
        public virtual Roles? Role { get; set; }

        /// <summary>
        /// Gets or sets the actions.
        /// </summary>
        /// <value>
        /// The actions.
        /// </value>
        [Computed]
        public virtual Actions? Action { get; set; }
    }
}
