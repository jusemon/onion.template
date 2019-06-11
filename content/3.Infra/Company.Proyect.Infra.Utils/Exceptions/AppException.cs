namespace Company.Proyect.Infra.Utils.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    public class AppException : Exception
    {

        public AppExceptionTypes Type { get; private set; }

        public AppException(AppExceptionTypes type) : base()
        {
            this.Type = type;
        }

        public AppException(AppExceptionTypes type, string message) : base(message)
        {
            this.Type = type;
        }

        public AppException(AppExceptionTypes type, string message, Exception innerException) : base(message, innerException)
        {
            this.Type = type;
        }

        public AppException(AppExceptionTypes type, SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            this.Type = type;
        }
    }
}
