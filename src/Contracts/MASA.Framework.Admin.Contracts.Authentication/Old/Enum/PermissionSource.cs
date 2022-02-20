namespace MASA.Framework.Admin.Contracts.Authentication.Old.Enum;

public enum PermissionSource
{
    /// <summary>
    /// assign current role
    /// </summary>
    Endow = 1,

    /// <summary>
    /// Permissions inherited from subroles
    /// </summary>
    Inherit
}
