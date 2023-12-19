using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using Module = Autofac.Module;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.InternalCommands
{
    /// <summary>
    /// Represents a module responsible for configuring internal commands in the administration module.
    /// </summary>
    internal class InternalCommandsModule : Module
    {
        private readonly BiDictionary<string, Type> _internalCommandsMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalCommandsModule"/> class.
        /// </summary>
        /// <param name="internalCommandsMap">The internal commands map.</param>
        public InternalCommandsModule(BiDictionary<string, Type> internalCommandsMap)
        {
            _internalCommandsMap = internalCommandsMap;
        }

        /// <summary>
        /// Loads the internal commands module into the Autofac container,
        /// registering all implementations of the <see cref="IInternalCommandsMapper"/> interface.
        /// </summary>
        /// <param name="builder">The container builder.</param>
        /// <exception cref="ApplicationException">Internal Commands not mapped.</exception>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InternalCommandsMapper>()
                .As<IInternalCommandsMapper>()
                .FindConstructorsWith(new AllConstructorFinder())
                .WithParameter("internalCommandsMap", _internalCommandsMap)
                .SingleInstance();

            this.CheckMappings();
        }

        private void CheckMappings()
        {
            var internalCommands = Assemblies.Application
                .GetTypes()
                .Where(x => x.BaseType != null &&
                            (
                                (x.BaseType.IsGenericType &&
                                x.BaseType.GetGenericTypeDefinition() == typeof(InternalCommandBase<>)) ||
                                x.BaseType == typeof(InternalCommandBase)))
                .ToList();

            List<Type> notMappedInternalCommands = new List<Type>();
            foreach (var internalCommand in internalCommands)
            {
                _internalCommandsMap.TryGetBySecond(internalCommand, out var name);

                if (name == null)
                {
                    notMappedInternalCommands.Add(internalCommand);
                }
            }

            if (notMappedInternalCommands.Any())
            {
                throw new ApplicationException($"Internal Commands {notMappedInternalCommands.Select(x => x.FullName).Aggregate((x, y) => x + "," + y)} not mapped");
            }
        }
    }
}