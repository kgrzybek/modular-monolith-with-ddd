﻿namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Processing.Inbox;

public class InboxMessageDto
{
    public Guid Id { get; set; }

    public string? Type { get; set; }

    public string? Data { get; set; }
}