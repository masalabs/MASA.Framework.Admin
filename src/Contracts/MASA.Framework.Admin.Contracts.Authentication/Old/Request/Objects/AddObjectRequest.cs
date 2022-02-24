﻿using MASA.Framework.Admin.Contracts.Authentication.Old.Enum;

namespace MASA.Framework.Admin.Contracts.Authentication.Old.Request.Objects;

public class AddObjectRequest
{
    public string Code { get; set; }

    public string Name { get; set; }

    public ObjectType ObjectType { get; set; }

    public List<ObjectItemRequest> ObjectItems { get; set; } = new();

    public AddObjectRequest()
    {

    }

    public AddObjectRequest(
        string code,
        string name,
        ObjectType objectType)
    {
        Code = code;
        Name = name;
        ObjectType = objectType;
    }
}