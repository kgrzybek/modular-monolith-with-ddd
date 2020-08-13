﻿using System;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.CreateUser
{
    public class CreateUserCommand : InternalCommandBase<Guid>
    {
        [JsonConstructor]
        public CreateUserCommand(Guid id, UserRegistrationId userRegistrationId) : base(id)
        {
            UserRegistrationId = userRegistrationId;
        }

        public UserRegistrationId UserRegistrationId { get; }
    }
}