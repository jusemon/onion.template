namespace Company.Project.Application.Interfaces.Security.DTOs
{
    /// <summary>
    /// User Login class. 
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string? Password { get; set; }
    }
}
