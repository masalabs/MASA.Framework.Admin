namespace MASA.Framework.Admin.Blog.Components;

/// <inheritdoc />
public partial class DefaultTextField<TValue> : MTextField<TValue>
{
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        Dense = true;
        HideDetails = true;
        Outlined = true;
        Clearable = true;

        await base.SetParametersAsync(parameters);
    }
}