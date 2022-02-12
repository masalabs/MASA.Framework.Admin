namespace MASA.Framework.Admin.Management.Components;

public partial class DefaultSelect<TItem, TItemValue, TValue> : MSelect<TItem, TItemValue, TValue>
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