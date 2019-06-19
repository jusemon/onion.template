namespace Company.Project.Infra.IoC.ConfigureServicesExtensions
{
    using System;
    using System.Linq;

    /// <summary>
    /// Configure Services Helper class. 
    /// </summary>
    public static class ConfigureServicesHelper
    {
        /// <summary>
        /// Shoulds the register.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="baseType">Type of the base.</param>
        /// <returns></returns>
        public static bool ShouldRegister(Type a, Type b, Type baseType)
        {
            if (!a.IsInterface || !b.IsClass) return false;
            bool ImplementsBaseService(Type i) => i.IsGenericType && i.GetGenericTypeDefinition() == baseType;
            return ImplementsBaseService(a) || a.GetInterfaces().Any(ImplementsBaseService);
        }
    }
}
