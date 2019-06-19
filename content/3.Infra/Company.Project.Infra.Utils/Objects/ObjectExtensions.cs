namespace Company.Project.Infra.Utils.Objects
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Object Extensions class. 
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts to object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType
                         .GetProperty(item.Key)
                         .SetValue(someObject, item.Value, null);
            }

            return someObject;
        }

        /// <summary>
        /// Ases the dictionary.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns></returns>
        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }

        /// <summary>
        /// Ases the dictionary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns></returns>
        public static IDictionary<string, T> AsDictionary<T>(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => (T)propInfo.GetValue(source, null)
            );
        }

        /// <summary>
        /// Converts to dynamic.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static dynamic ToDynamic(this object source)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source.GetType()))
            {
                expando.Add(property.Name, property.GetValue(source));
            }
            return expando as ExpandoObject;
        }
    }
}
