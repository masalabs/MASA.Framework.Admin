﻿namespace MASA.Framework.Admin.Contracts.Base.Commands;

public record AdminCommand : Command
{
    public Guid UserId { get; set; }
}