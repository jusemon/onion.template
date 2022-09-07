namespace Company.Project.Application.Interfaces.Generics
{
    using Infra.Utils.Exceptions;

    /// <summary>
    /// Response class. 
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class Response<TResult>
    {
        /// <summary>
        /// Gets or sets the exception message.
        /// </summary>
        /// <value>
        /// The exception message.
        /// </value>
        public string? ExceptionMessage { get; set; }

        /// <summary>
        /// Gets or sets the type of the exception.
        /// </summary>
        /// <value>
        /// The type of the exception.
        /// </value>
        public AppExceptionTypes ExceptionType { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public TResult? Result { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess { get; set; }
    }
}
