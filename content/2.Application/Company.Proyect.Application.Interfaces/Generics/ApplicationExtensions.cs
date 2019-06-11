namespace Company.Proyect.Application.Interfaces.Generics
{
    using Base;
    using Domain.Entities.Generics.Base;
    using Infra.Utils.Exceptions;
    using System;

    public static class ApplicationExtensions
    {
        public static Response<TResult> Try<TEntity, TResult>(
            this IBaseApplication<TEntity> baseApplication,
            Func<TResult> action) where TEntity : BaseEntity, new()
        {
            var response = new Response<TResult>();
            try
            {
                response.Result = action();
                response.IsSuccess = true;
            }
            catch (AppException aex)
            {
                var ex = aex.InnerException ?? aex;
                response.ExceptionMessage = ex.Message;
                response.ExceptionType = aex.Type;
            }
            catch (Exception ex)
            {
                var e = ex.InnerException ?? ex;
                response.ExceptionMessage = e.Message;
                response.ExceptionType = AppExceptionTypes.Generic;
            }
            return response;
        }
    }
}
