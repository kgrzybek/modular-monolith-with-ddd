using System.Reflection;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.ArchTests.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using NetArchTest.Rules;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.ArchTests.Domain
{
    public class DomainTests : TestBase
    {
        [Test]
        public void DomainEvent_Should_Be_Immutable()
        {
            var types = Types.InAssembly(DomainAssembly)
                .That()
                    .Inherit(typeof(DomainEventBase))
                        .Or()
                    .ImplementInterface(typeof(IDomainEvent))
                .GetTypes();

            AssertAreImmutable(types);
        }

        [Test]
        public void ValueObject_Should_Be_Immutable()
        {
            var types = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(ValueObject))
                .GetTypes();

            AssertAreImmutable(types);
        }

        [Test]
        public void Entity_Which_Is_Not_Aggregate_Root_Cannot_Have_Public_Members()
        {
            var types = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(Entity))
                .And().DoNotImplementInterface(typeof(IAggregateRoot)).GetTypes();

            const BindingFlags bindingFlags = BindingFlags.DeclaredOnly |
                                              BindingFlags.Public |
                                              BindingFlags.Instance |
                                              BindingFlags.Static;

            List<Type> failingTypes = [];
            foreach (var type in types)
            {
                var publicFields = type.GetFields(bindingFlags);
                var publicProperties = type.GetProperties(bindingFlags);
                var publicMethods = type.GetMethods(bindingFlags);

                if (publicFields.Any() || publicProperties.Any() || publicMethods.Any())
                {
                    failingTypes.Add(type);
                }
            }

            AssertFailingTypes(failingTypes);
        }

        [Test]
        public void Entity_Cannot_Have_Reference_To_Other_AggregateRoot()
        {
            var entityTypes = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(Entity)).GetTypes();

            var aggregateRoots = Types.InAssembly(DomainAssembly)
                .That().ImplementInterface(typeof(IAggregateRoot)).GetTypes().ToList();

            const BindingFlags bindingFlags = BindingFlags.DeclaredOnly |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Instance;

            List<Type> failingTypes = [];
            foreach (var type in entityTypes)
            {
                var fields = type.GetFields(bindingFlags);

                foreach (var field in fields)
                {
                    if (aggregateRoots.Contains(field.FieldType) ||
                        field.FieldType.GenericTypeArguments.Any(x => aggregateRoots.Contains(x)))
                    {
                        failingTypes.Add(type);
                        break;
                    }
                }

                var properties = type.GetProperties(bindingFlags);
                foreach (var property in properties)
                {
                    if (aggregateRoots.Contains(property.PropertyType) ||
                        property.PropertyType.GenericTypeArguments.Any(x => aggregateRoots.Contains(x)))
                    {
                        failingTypes.Add(type);
                        break;
                    }
                }
            }

            AssertFailingTypes(failingTypes);
        }

        [Test]
        public void Entity_Should_Have_Parameterless_Private_Constructor()
        {
            var entityTypes = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(Entity)).GetTypes();

            List<Type> failingTypes = [];
            foreach (var entityType in entityTypes)
            {
                bool hasPrivateParameterlessConstructor = false;
                var constructors = entityType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var constructorInfo in constructors)
                {
                    if (constructorInfo.IsPrivate && constructorInfo.GetParameters().Length == 0)
                    {
                        hasPrivateParameterlessConstructor = true;
                    }
                }

                if (!hasPrivateParameterlessConstructor)
                {
                    failingTypes.Add(entityType);
                }
            }

            AssertFailingTypes(failingTypes);
        }

        [Test]
        public void Domain_Object_Should_Have_Only_Private_Constructors()
        {
            var domainObjectTypes = Types.InAssembly(DomainAssembly)
                .That()
                        .Inherit(typeof(Entity))
                    .Or()
                        .Inherit(typeof(ValueObject))
                .GetTypes();

            List<Type> failingTypes = [];
            foreach (var domainObjectType in domainObjectTypes)
            {
                var constructors = domainObjectType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                foreach (var constructorInfo in constructors)
                {
                    if (!constructorInfo.IsPrivate)
                    {
                        failingTypes.Add(domainObjectType);
                    }
                }
            }

            AssertFailingTypes(failingTypes);
        }

        [Test]
        public void ValueObject_Should_Have_Private_Constructor_With_Parameters_For_His_State()
        {
            List<Type> excludedFromCheck = [typeof(PriceList)];

            var valueObjects = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(ValueObject))
                .GetTypes()
                .Where(x => !excludedFromCheck.Contains(x))
                .ToList();

            List<Type> failingTypes = [];
            foreach (var entityType in valueObjects)
            {
                bool hasExpectedConstructor = false;

                const BindingFlags bindingFlags = BindingFlags.DeclaredOnly |
                                                  BindingFlags.Public |
                                                  BindingFlags.Instance;
                var names = entityType.GetFields(bindingFlags).Select(x => x.Name.ToLower()).ToList();
                var propertyNames = entityType.GetProperties(bindingFlags).Select(x => x.Name.ToLower()).ToList();
                names.AddRange(propertyNames);
                var constructors = entityType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var constructorInfo in constructors)
                {
                    var parameters = constructorInfo.GetParameters().Select(x => x.Name.ToLower()).ToList();

                    if (names.Intersect(parameters).Count() == names.Count)
                    {
                        hasExpectedConstructor = true;
                        break;
                    }
                }

                if (!hasExpectedConstructor)
                {
                    failingTypes.Add(entityType);
                }
            }

            AssertFailingTypes(failingTypes);
        }

        [Test]
        public void DomainEvent_Should_Have_DomainEventPostfix()
        {
            var result = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(DomainEventBase))
                .Or()
                .ImplementInterface(typeof(IDomainEvent))
                .Should().HaveNameEndingWith("DomainEvent")
                .GetResult();

            AssertArchTestResult(result);
        }

        [Test]
        public void BusinessRule_Should_Have_RulePostfix()
        {
            var result = Types.InAssembly(DomainAssembly)
                .That()
                .ImplementInterface(typeof(IBusinessRule))
                .Should().HaveNameEndingWith("Rule")
                .GetResult();

            AssertArchTestResult(result);
        }
    }
}