namespace Company.Project.Domain.Entities.Security
{
    using Generics.Base;

    /// <summary>
    /// Action class. 
    /// </summary>
    /// <seealso cref="Company.Project.Domain.Entities.Generics.Base.BaseEntity" />
    public class Menu : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the action identifier.
        /// </summary>
        /// <value>
        /// The action identifier.
        /// </value>
        public ulong ActionId { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public virtual Activity? Action { get; set; }
    }
}
