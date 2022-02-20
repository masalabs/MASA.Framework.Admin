namespace MASA.Framework.Admin.Blog.Components
{
    public class DefaultSwitch : MSwitch
    {
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            HideDetails = "auto";
            Style = "margin-top:0px";

            await base.SetParametersAsync(parameters);
        }
    }
}
