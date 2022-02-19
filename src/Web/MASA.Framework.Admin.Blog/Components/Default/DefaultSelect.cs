namespace MASA.Framework.Admin.Blog.Components;

public partial class DefaultSelect<TItem, TItemValue, TValue> : MSelect<TItem, TItemValue, TValue>
{
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        Dense = true;
        HideDetails = true;
        Outlined = true;
        Clearable = true;

        // TODO: not work, bug of MSelect
        MenuProps = (props) => props.OffsetY = true; 
    }
}