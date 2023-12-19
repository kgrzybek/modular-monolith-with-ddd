using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Serialization
{
    /// <summary>
    /// Custom contract resolver that includes all properties for serialization and deserialization.
    /// </summary>
    public class AllPropertiesContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Creates a list of <see cref="JsonProperty"/> objects for all properties of the specified type.
        /// </summary>
        /// <param name="type">The type to create properties for.</param>
        /// <param name="memberSerialization">The member serialization mode.</param>
        /// <returns>A list of <see cref="JsonProperty"/> objects.</returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = type.GetProperties(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance)
                .Select(p => this.CreateProperty(p, memberSerialization))
                .ToList();

            properties.ForEach(p =>
            {
                p.Writable = true;
                p.Readable = true;
            });

            return properties;
        }
    }
}