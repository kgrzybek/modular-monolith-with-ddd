﻿namespace CompanyName.MyMeetings.Contracts.V1.Users.Roles;

public class PermissionDto
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}