﻿using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations.Events;

public class UserRegistrationConfirmedDomainEvent : DomainEventBase
{
    public UserRegistrationConfirmedDomainEvent(UserRegistrationId userRegistrationId)
    {
        UserRegistrationId = userRegistrationId;
    }

    public UserRegistrationId UserRegistrationId { get; }
}