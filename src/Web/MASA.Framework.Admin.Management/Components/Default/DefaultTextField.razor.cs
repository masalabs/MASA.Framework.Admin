namespace MASA.Framework.Admin.Management.Components;

/// <inheritdoc />
public partial class DefaultTextField<TValue> : MTextField<TValue>
{
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        Dense = true;
        HideDetails = true;
        Outlined = true;
        Clearable = true;
    }
}