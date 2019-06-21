namespace Company.Project.Domain.Entities.Security
{
    using Dapper.Contrib.Extensions;
    using Generics.Base;

    /// <summary>
    /// User class.
    /// </summary>
    /// <seealso cref="Company.Project.Domain.Entities.Generics.Base.BaseEntity" />
    [Table("Users")]
    public class Users : BaseEntity
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public long RoleId { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        [Computed]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        [Computed]
        public virtual Roles Roles { get; set; }
    }
}
