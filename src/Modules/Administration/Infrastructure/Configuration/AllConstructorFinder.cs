using System.Collections.Concurrent;
using System.Reflection;
using Autofac.Core.Activators.Reflection;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration
{
    /// <summary>
    /// Represents a class that finds all constructors for a given type.
    /// </summary>
    internal class AllConstructorFinder : IConstructorFinder
    {
        private static readonly ConcurrentDictionary<Type, ConstructorInfo[]> Cache =
            new ConcurrentDictionary<Type, ConstructorInfo[]>();

        /// <summary>
        /// Finds all constructors for the specified target type.
        /// </summary>
        /// <param name="targetType">The target type to find constructors for.</param>
        /// <returns>An array of <see cref="ConstructorInfo"/> objects representing the constructors found.</returns>
        public ConstructorInfo[] FindConstructors(Type targetType)
        {
            var result = Cache.GetOrAdd(
                targetType,
                t => t.GetTypeInfo().DeclaredConstructors.ToArray());

            return result.Length > 0 ? result : throw new NoConstructorsFoundException(targetType, this);
        }
    }
}