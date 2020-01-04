using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccess.ArchTests.SeedWork;
using MediatR;
using NetArchTest.Rules;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.UserAccess.ArchTests.Application
{
    [TestFixture]
    public class ApplicationTests : TestBase
    {
        [Test]
        public void Command_Should_Be_Immutable()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That().Inherit(typeof(CommandBase))
                .Or().Inherit(typeof(InternalCommandBase))
                .Or().ImplementInterface(typeof(ICommand))
                .Or().ImplementInterface(typeof(ICommand<>))
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
        public void InternalCommands_Should_Not_Be_Public()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .That()
                .Inherit(typeof(InternalCommandBase))
                .Should()
                .NotBePublic()
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
                .Should().NotBePublic().GetResult().FailingTypes;

            AssertFailingTypes(types); 
        }

        [Test]
        public void InternalCommand_Should_Have_Internal_Constructor_With_JsonConstructorAttribute()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That().Inherit(typeof(InternalCommandBase)).GetTypes();

            var failingTypes = new List<Type>();

            foreach (var type in types)
            {
                bool hasJsonConstructorDefined = false;
                var constructors = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
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

            List<Type> failingTypes = new List<Type>();
            foreach (var type in types)
            {
                bool isCommandHandler = type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(ICommandHandler<>));
                bool isQueryHandler = type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));
                if (!isCommandHandler && !isQueryHandler)
                {
                    failingTypes.Add(type);
                }
            }
            
            AssertFailingTypes(failingTypes);      
        }
    }
}