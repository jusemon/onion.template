namespace Company.Project.Application.Interfaces.Generics
{
    using Base;
    using Domain.Entities.Generics.Base;
    using Infra.Utils.Exceptions;
    using System;

    /// <summary>
    /// Application Extensions class. 
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Tries the specified action.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="baseApplication">The base application.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static Response<TResult> Try<TResult>(
            Func<TResult> action)
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

        /// <summary>
        /// Tries the specified asyncronous action.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="baseApplication">The base application.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static async Task<Response<TResult>> TryAsync<TResult>(
            Func<Task<TResult>> action)
        {
            var response = new Response<TResult>();
            try
            {
                response.Result = await action();
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
