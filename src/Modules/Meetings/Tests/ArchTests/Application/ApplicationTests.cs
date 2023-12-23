using System.Reflection;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.ArchTests.SeedWork;
using FluentValidation;
using MediatR;
using NetArchTest.Rules;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.ArchTests.Application
{
    [TestFixture]
    public class ApplicationTests : TestBase
    {
        [Test]
        public void Command_Should_Be_Immutable()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That()
                .Inherit(typeof(CommandBase))
                .Or()
                .Inherit(typeof(CommandBase<>))
                .Or()
                .Inherit(typeof(InternalCommandBase))
                .Or()
                .Inherit(typeof(InternalCommandBase<>))
                .Or()
                .ImplementInterface(typeof(ICommand))
                .Or()
                .ImplementInterface(typeof(ICommand<>))
                .GetTypes();

            AssertAreImmutable(types);
        }

        [Test]
        public void Query_Should_Be_Immutable()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That().ImplementInterface(typeof(IQuery<>)).GetTypes();

            AssertAreImmutable(types);
        }

        [Test]
        public void CommandHandler_Should_Have_Name_EndingWith_CommandHandler()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<>))
                    .Or()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .And()
                .DoNotHaveNameMatching(".*Decorator.*").Should()
                .HaveNameEndingWith("CommandHandler")
                .GetResult();

            AssertArchTestResult(result);
        }

        [Test]
        public void QueryHandler_Should_Have_Name_EndingWith_QueryHandler()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .Should()
                .HaveNameEndingWith("QueryHandler")
                .GetResult();

            AssertArchTestResult(result);
        }

        [Test]
        public void Command_And_Query_Handlers_Should_Not_Be_Public()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That()
                    .ImplementInterface(typeof(IQueryHandler<,>))
                        .Or()
                    .ImplementInterface(typeof(ICommandHandler<>))
                        .Or()
                    .ImplementInterface(typeof(ICommandHandler<,>))
                .Should().NotBePublic().GetResult().FailingTypes;

            AssertFailingTypes(types);
        }

        [Test]
        public void Validator_Should_Have_Name_EndingWith_Validator()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .That()
                .Inherit(typeof(AbstractValidator<>))
                .Should()
                .HaveNameEndingWith("Validator")
                .GetResult();

            AssertArchTestResult(result);
        }

        [Test]
        public void Validators_Should_Not_Be_Public()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That()
                .Inherit(typeof(AbstractValidator<>))
                .Should().NotBePublic().GetResult().FailingTypes;

            AssertFailingTypes(types);
        }

        [Test]
        public void InternalCommand_Should_Have_Constructor_With_JsonConstructorAttribute()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That()
                .Inherit(typeof(InternalCommandBase))
                .Or()
                .Inherit(typeof(InternalCommandBase<>))
                .GetTypes();

            List<Type> failingTypes = [];

            foreach (var type in types)
            {
                bool hasJsonConstructorDefined = false;
                var constructors = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                foreach (var constructorInfo in constructors)
                {
                    var jsonConstructorAttribute = constructorInfo.GetCustomAttributes(typeof(JsonConstructorAttribute), false);
                    if (jsonConstructorAttribute.Length > 0)
                    {
                        hasJsonConstructorDefined = true;
                        break;
                    }
                }

                if (!hasJsonConstructorDefined)
                {
                    failingTypes.Add(type);
                }
            }

            AssertFailingTypes(failingTypes);
        }

        [Test]
        public void MediatR_RequestHandler_Should_NotBe_Used_Directly()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That().DoNotHaveName("ICommandHandler`1")
                .Should().ImplementInterface(typeof(IRequestHandler<>))
                .GetTypes();

            List<Type> failingTypes = [];
            foreach (var type in types)
            {
                bool isCommandHandler = type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(ICommandHandler<>));
                bool isCommandWithResultHandler = type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));
                bool isQueryHandler = type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));
                if (!isCommandHandler && !isCommandWithResultHandler && !isQueryHandler)
                {
                    failingTypes.Add(type);
                }
            }

            AssertFailingTypes(failingTypes);
        }

        [Test]
        public void Command_With_Result_Should_Not_Return_Unit()
        {
            Type commandWithResultHandlerType = typeof(ICommandHandler<,>);
            IEnumerable<Type> types = Types.InAssembly(ApplicationAssembly)
                .That().ImplementInterface(commandWithResultHandlerType)
                .GetTypes().ToList();

            List<Type> failingTypes = [];
            foreach (Type type in types)
            {
                Type interfaceType = type.GetInterface(commandWithResultHandlerType.Name);
                if (interfaceType?.GenericTypeArguments[1] == typeof(Unit))
                {
                    failingTypes.Add(type);
                }
            }

            AssertFailingTypes(failingTypes);
        }
    }
}