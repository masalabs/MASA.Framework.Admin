﻿namespace MASA.Framework.Admin.Contracts.Authentication.Request.Roles;

public record EditRoleRequest
{
    public Guid RuleId { get; set; }

    public string Name { get; set; } = default!;

    public string Describe { get; set; } = default!;
}