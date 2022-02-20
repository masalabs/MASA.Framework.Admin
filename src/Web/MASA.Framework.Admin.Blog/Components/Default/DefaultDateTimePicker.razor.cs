namespace MASA.Framework.Admin.Blog.Components;

public partial class DefaultDateTimePicker<TValue>: PDateTimePicker<TValue>
{
    [CascadingParameter(Name = "TimeZoneOffset")]
    public TimeSpan Offset { get; set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        Clearable = true;
        Dense = true;
        DefaultSelectedValue = DateTime.UtcNow;
        HideDetails = "auto";
        Outlined = true;

        Format = "yyyy-MM-dd HH:mm:ss";
        TimeZoneOffset = Offset;

        await base.SetParametersAsync(parameters);
    }
}