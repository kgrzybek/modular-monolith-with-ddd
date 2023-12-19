namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure
{
    /// <summary>
    /// Represents a bi-directional dictionary that allows mapping between two types.
    /// </summary>
    /// <typeparam name="TFirst">The type of the first element in the dictionary.</typeparam>
    /// <typeparam name="TSecond">The type of the second element in the dictionary.</typeparam>
    public class BiDictionary<TFirst, TSecond>
    {
        private readonly Dictionary<TFirst, TSecond> _firstToSecond = new Dictionary<TFirst, TSecond>();

        private readonly Dictionary<TSecond, TFirst> _secondToFirst = new Dictionary<TSecond, TFirst>();

        /// <summary>
        /// Adds a mapping between the first and second elements.
        /// </summary>
        /// <param name="first">The first element.</param>
        /// <param name="second">The second element.</param>
        /// <exception cref="ArgumentException">Thrown when a duplicate first or second element is added.</exception>
        public void Add(TFirst first, TSecond second)
        {
            if (_firstToSecond.ContainsKey(first) ||
                _secondToFirst.ContainsKey(second))
            {
                throw new ArgumentException("Duplicate first or second");
            }

            _firstToSecond.Add(first, second);
            _secondToFirst.Add(second, first);
        }

        /// <summary>
        /// Tries to get the second element associated with the specified first element.
        /// </summary>
        /// <param name="first">The first element.</param>
        /// <param name="second">When this method returns, contains the second element associated with the specified first element, if found; otherwise, the default value for the type of the second element.</param>
        /// <returns><c>true</c> if the dictionary contains an element with the specified first element; otherwise, <c>false</c>.</returns>
        public bool TryGetByFirst(TFirst first, out TSecond second)
        {
            return _firstToSecond.TryGetValue(first, out second);
        }

        /// <summary>
        /// Tries to get the first element associated with the specified second element.
        /// </summary>
        /// <param name="second">The second element.</param>
        /// <param name="first">When this method returns, contains the first element associated with the specified second element, if found; otherwise, the default value for the type of the first element.</param>
        /// <returns><c>true</c> if the dictionary contains an element with the specified second element; otherwise, <c>false</c>.</returns>
        public bool TryGetBySecond(TSecond second, out TFirst first)
        {
            return _secondToFirst.TryGetValue(second, out first);
        }
    }
}