namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands
{
    /// <summary>
    /// Represents a mapper for internal commands.
    /// </summary>
    public interface IInternalCommandsMapper
    {
        /// <summary>
        /// Gets the name of the specified command type.
        /// </summary>
        /// <param name="type">The type of the command.</param>
        /// <returns>The name of the command type.</returns>
        string GetName(Type type);

        /// <summary>
        /// Gets the type of the command with the specified name.
        /// </summary>
        /// <param name="name">The name of the command type.</param>
        /// <returns>The type of the command.</returns>
        Type GetType(string name);
    }
}