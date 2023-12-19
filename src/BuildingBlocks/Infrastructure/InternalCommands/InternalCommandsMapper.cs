namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands
{
    /// <summary>
    /// Maps internal commands to their corresponding names and types.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="InternalCommandsMapper"/> class.
    /// </remarks>
    /// <param name="internalCommandsMap">The internal commands map.</param>
    public class InternalCommandsMapper(BiDictionary<string, Type> internalCommandsMap) : IInternalCommandsMapper
    {
        private readonly BiDictionary<string, Type> _internalCommandsMap = internalCommandsMap;

        /// <inheritdoc/>
        public string GetName(Type type)
        {
            return _internalCommandsMap.TryGetBySecond(type, out var name) ? name : null;
        }

        /// <inheritdoc/>
        public Type GetType(string name)
        {
            return _internalCommandsMap.TryGetByFirst(name, out var type) ? type : null;
        }
    }
}