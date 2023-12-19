namespace CompanyName.MyMeetings.BuildingBlocks.Domain
{
    /// <summary>
    /// Base class for typed ID values.
    /// </summary>
    public abstract class TypedIdValueBase : IEquatable<TypedIdValueBase>
    {
        /// <summary>
        /// Gets the underlying value of the typed ID.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedIdValueBase"/> class.
        /// </summary>
        /// <param name="value">The value of the typed ID.</param>
        protected TypedIdValueBase(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidOperationException("Id value cannot be empty!");
            }

            Value = value;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return obj is TypedIdValueBase other && Equals(other);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <inheritdoc/>
        public bool Equals(TypedIdValueBase other)
        {
            return this.Value == other?.Value;
        }

        /// <summary>
        /// Determines whether two <see cref="TypedIdValueBase"/> objects are equal.
        /// </summary>
        /// <param name="obj1">The first object to compare.</param>
        /// <param name="obj2">The second object to compare.</param>
        /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(TypedIdValueBase obj1, TypedIdValueBase obj2)
        {
            if (object.Equals(obj1, null))
            {
                if (object.Equals(obj2, null))
                {
                    return true;
                }

                return false;
            }

            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Determines whether two <see cref="TypedIdValueBase"/> objects are not equal.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns><c>true</c> if the objects are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(TypedIdValueBase x, TypedIdValueBase y)
        {
            return !(x == y);
        }
    }
}