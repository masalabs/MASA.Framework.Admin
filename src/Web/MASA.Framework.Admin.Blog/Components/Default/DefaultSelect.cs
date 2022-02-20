namespace MASA.Framework.Admin.Blog.Components;

public partial class DefaultSelect<TItem, TItemValue, TValue> : MSelect<TItem, TItemValue, TValue>
{
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        Dense = true;
        HideDetails = true;
        Outlined = true;
        Clearable = true;
        MenuProps = (props) => props.OffsetY = true;

        await base.SetParametersAsync(parameters);
    }
}