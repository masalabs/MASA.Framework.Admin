namespace MASA.Framework.Admin.Blog.Components
{
    public class DefaultGenericColumnRender : GenericColumnRender
    {
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            DateFormat = "yyyy-MM-dd";
            TimeFormat = "HH:mm:ss";
        }
    }
}
