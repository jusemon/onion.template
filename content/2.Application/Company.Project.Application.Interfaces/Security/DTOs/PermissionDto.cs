namespace Company.Project.Application.Interfaces.Security.DTOs
{

    /// <summary>
    /// Permission class. 
    /// </summary>
    public class PermissionDto
    {
        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public uint RoleId { get; set; }

        /// <summary>
        /// Gets or sets the action identifier.
        /// </summary>
        /// <value>
        /// The action identifier.
        /// </value>
        public uint ActionId { get; set; }
    }
}
