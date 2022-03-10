﻿namespace Masa.Framework.Sdks.Authentication.Response.Base;

public class ApiResultResponseBase
{
    public bool Success { get; }

    public string Message { get; }

    protected ApiResultResponseBase(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public static ApiResultResponseBase ResponseSuccess(string message) => new(true, message);

    public static ApiResultResponseBase ResponseLose(string message) => new(false, message);
}
