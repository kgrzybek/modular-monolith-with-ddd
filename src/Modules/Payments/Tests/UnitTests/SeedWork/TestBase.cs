using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.SeedWork
{
    public abstract class TestBase
    {
        public static T AssertPublishedDomainEvent<T>(Entity aggregate)
            where T : IDomainEvent
        {
            var domainEvent = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<T>().SingleOrDefault();

            if (domainEvent == null)
            {
                throw new Exception($"{typeof(T).Name} event not published");
            }

            return domainEvent;
        }

        public static T AssertPublishedDomainEvent<T>(AggregateRoot aggregate)
            where T : IDomainEvent
        {
            var domainEvent = aggregate.GetDomainEvents().OfType<T>().SingleOrDefault();

            if (domainEvent == null)
            {
                throw new Exception($"{typeof(T).Name} event not published");
            }

            return domainEvent;
        }

        public static void AssertDomainEventNotPublished<T>(AggregateRoot aggregate)
            where T : IDomainEvent
        {
            var domainEvent = aggregate.GetDomainEvents().OfType<T>().SingleOrDefault();
            Assert.That(domainEvent, Is.Null);
        }

        public static List<T> AssertPublishedDomainEvents<T>(Entity aggregate)
            where T : IDomainEvent
        {
            var domainEvents = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<T>().ToList();

            if (!domainEvents.Any())
            {
                throw new Exception($"{typeof(T).Name} event not published");
            }

            return domainEvents;
        }

        public static List<T> AssertPublishedDomainEvents<T>(AggregateRoot aggregate)
            where T : IDomainEvent
        {
            var domainEvents = aggregate.GetDomainEvents().OfType<T>().ToList();

            if (!domainEvents.Any())
            {
                throw new Exception($"{typeof(T).Name} event was not published");
            }

            return domainEvents;
        }

        public static void AssertBrokenRule<TRule>(TestDelegate testDelegate)
            where TRule : class, IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var businessRuleValidationException = Assert.Catch<BusinessRuleValidationException>(testDelegate, message);
            if (businessRuleValidationException != null)
            {
                Assert.That(businessRuleValidationException.BrokenRule, Is.TypeOf<TRule>(), message);
            }
        }

        [TearDown]
        public void AfterEachTest()
        {
            SystemClock.Reset();
        }
    }
}