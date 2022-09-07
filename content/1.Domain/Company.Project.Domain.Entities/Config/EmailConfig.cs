namespace Company.Project.Domain.Entities.Config
{
    /// <summary>
    /// Email Configuration class. 
    /// </summary>
    public class EmailConfig
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string ApiKey { get; set; } = null!;

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        /// <value>
        /// The sender.
        /// </value>
        public string Sender { get; set; } = null!;
    }
}
