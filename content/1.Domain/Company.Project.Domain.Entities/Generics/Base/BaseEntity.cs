namespace Company.Project.Domain.Entities.Generics.Base
{
    using System;
    using Security;

    /// <summary>
    /// Base Entity class. 
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public uint Id { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public uint CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the last updated at.
        /// </summary>
        /// <value>
        /// The last updated at.
        /// </value>
        public DateTime? LastUpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last updated by.
        /// </summary>
        /// <value>
        /// The last updated by.
        /// </value>
        public uint? LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created by user.
        /// </summary>
        /// <value>
        /// The created by user.
        /// </value>
        public virtual User? CreatedByUser { get; set; }

        /// <summary>
        /// Gets or sets the last updated by user.
        /// </summary>
        /// <value>
        /// The last updated by user.
        /// </value>
        public virtual User? LastUpdatedByUser { get; set; }
    }
}
