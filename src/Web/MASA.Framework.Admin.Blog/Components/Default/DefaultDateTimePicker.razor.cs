namespace MASA.Framework.Admin.Blog.Components;

public partial class DefaultDateTimePicker<TValue>: PDateTimePicker<TValue>
{
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        Clearable = true;
        Dense = true;
        HideDetails = true;
        Outlined = true;
    }
}