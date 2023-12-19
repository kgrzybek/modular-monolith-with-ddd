using CompanyName.MyMeetings.BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure
{
    /// <summary>
    /// Converts a typed ID value to a Guid and vice versa.
    /// </summary>
    /// <typeparam name="TTypedIdValue">The type of the typed ID value.</typeparam>
    public class TypedIdValueConverter<TTypedIdValue> : ValueConverter<TTypedIdValue, Guid>
        where TTypedIdValue : TypedIdValueBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypedIdValueConverter{TTypedIdValue}"/> class.
        /// </summary>
        /// <param name="mappingHints">The mapping hints for the converter.</param>
        public TypedIdValueConverter(ConverterMappingHints mappingHints = null)
            : base(id => id.Value, value => Create(value), mappingHints)
        {
        }

        /// <summary>
        /// Creates a new instance of the typed ID value using the specified Guid.
        /// </summary>
        /// <param name="id">The Guid value.</param>
        /// <returns>A new instance of the typed ID value.</returns>
        private static TTypedIdValue Create(Guid id) => Activator.CreateInstance(typeof(TTypedIdValue), id) as TTypedIdValue;
    }
}