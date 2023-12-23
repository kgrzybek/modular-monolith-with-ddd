using System.Collections;
using System.Reflection;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.SeedWork
{
    public class DomainEventsTestHelper
    {
        public static List<IDomainEvent> GetAllDomainEvents(Entity aggregate)
        {
            List<IDomainEvent> domainEvents = [];

            if (aggregate.DomainEvents != null)
            {
                domainEvents.AddRange(aggregate.DomainEvents);
            }

            var fields = aggregate.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).Concat(aggregate.GetType().BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)).ToArray();

            foreach (var field in fields)
            {
                var isEntity = typeof(Entity).IsAssignableFrom(field.FieldType);

                if (isEntity)
                {
                    var entity = field.GetValue(aggregate) as Entity;
                    domainEvents.AddRange(GetAllDomainEvents(entity).ToList());
                }

                if (field.FieldType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                {
                    if (field.GetValue(aggregate) is IEnumerable enumerable)
                    {
                        foreach (var en in enumerable)
                        {
                            if (en is Entity entityItem)
                            {
                                domainEvents.AddRange(GetAllDomainEvents(entityItem));
                            }
                        }
                    }
                }
            }

            return domainEvents;
        }
    }
}