namespace Company.Project.Application.Interfaces.Security.DTOs
{

    /// <summary>
    /// Action class. 
    /// </summary>
    public class MenuDto
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
        public uint ActionId { get; set; }
    }
}
