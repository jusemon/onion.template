namespace Company.Proyect.Application.Interfaces.Generics
{
    using Infra.Utils.Exceptions;

    public class Response<TResult>
    {
        public string ExceptionMessage { get; set; }

        public AppExceptionTypes ExceptionType { get; set; }

        public TResult Result { get; set; }

        public bool IsSuccess { get; set; }
    }
}
