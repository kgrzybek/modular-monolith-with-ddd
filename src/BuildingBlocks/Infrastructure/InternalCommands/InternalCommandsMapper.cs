namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands
{
    public class InternalCommandsMapper : IInternalCommandsMapper
    {
        private readonly BiDictionary<string, Type> _internalCommandsMap;

        public InternalCommandsMapper(BiDictionary<string, Type> internalCommandsMap)
        {
            _internalCommandsMap = internalCommandsMap;
        }

        public string GetName(Type type)
        {
            return _internalCommandsMap.TryGetBySecond(type, out var name) ? name : null;
        }

        public Type GetType(string name)
        {
            return _internalCommandsMap.TryGetByFirst(name, out var type) ? type : null;
        }
    }
}