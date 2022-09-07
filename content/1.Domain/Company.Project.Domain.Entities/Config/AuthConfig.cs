namespace Company.Project.Domain.Entities.Config
{
    /// <summary>
    /// Authentication Configuration class. 
    /// </summary>
    public class AuthConfig
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; } = null!;

        /// <summary>
        /// Gets or sets the session timeout.
        /// </summary>
        /// <value>
        /// The session timeout.
        /// </value>
        public int SessionTimeout { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the in.
        /// </summary>
        /// <value>
        /// The in.
        /// </value>
        public int In { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; } = null!;
    }
}
