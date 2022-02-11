﻿namespace MASA.Framework.Admin.Contracts.Base.Extensions.Exceptions;

public class UserFriendlyException : Exception
{
    public string Code { get; set; }

    public UserFriendlyException(string message, string code = Const.Code.PARAMETER_ERROR) : base(message)
    {
        Code = code;
    }
}
