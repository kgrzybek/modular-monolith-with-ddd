namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure
{
    public class BiDictionary<TFirst, TSecond>
    {
        private readonly IDictionary<TFirst, TSecond> _firstToSecond = new Dictionary<TFirst, TSecond>();

        private readonly IDictionary<TSecond, TFirst> _secondToFirst = new Dictionary<TSecond, TFirst>();

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

        public bool TryGetByFirst(TFirst first, out TSecond second)
        {
            return _firstToSecond.TryGetValue(first, out second);
        }

        public bool TryGetBySecond(TSecond second, out TFirst first)
        {
            return _secondToFirst.TryGetValue(second, out first);
        }
    }
}