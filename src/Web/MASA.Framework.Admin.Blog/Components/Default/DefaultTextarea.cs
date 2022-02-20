namespace MASA.Framework.Admin.Blog.Components
{
    public class DefaultTextarea : MTextarea
    {
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            Clearable = true;
            Dense = true;
            HideDetails = "auto";
            Outlined = true;

            await base.SetParametersAsync(parameters);
        }
    }
}
