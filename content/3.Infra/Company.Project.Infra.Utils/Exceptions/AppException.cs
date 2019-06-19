namespace Company.Project.Infra.Utils.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Application Exception class. 
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class AppException : Exception
    {

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public AppExceptionTypes Type { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public AppException(AppExceptionTypes type) : base()
        {
            this.Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        public AppException(AppExceptionTypes type, string message) : base(message)
        {
            this.Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public AppException(AppExceptionTypes type, string message, Exception innerException) : base(message, innerException)
        {
            this.Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="serializationInfo">The serialization information.</param>
        /// <param name="streamingContext">The streaming context.</param>
        public AppException(AppExceptionTypes type, SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            this.Type = type;
        }
    }
}
