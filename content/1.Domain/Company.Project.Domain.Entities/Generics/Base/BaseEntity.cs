namespace Company.Project.Domain.Entities.Generics.Base
{
    using System;
    using Security;
    using Dapper.Contrib.Extensions;

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
        public long Id { get; set; }

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
        public long CreatedBy { get; set; }

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
        public long? LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created by user.
        /// </summary>
        /// <value>
        /// The created by user.
        /// </value>
        [Computed]
        public virtual Users? CreatedByUser { get; set; }

        /// <summary>
        /// Gets or sets the last updated by user.
        /// </summary>
        /// <value>
        /// The last updated by user.
        /// </value>
        [Computed]
        public virtual Users? LastUpdatedByUser { get; set; }
    }
}
