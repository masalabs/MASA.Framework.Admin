namespace MASA.Framework.Admin.Blog.Components
{
    public class MajorAction : PAction
    {
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            Color = "primary";

            if (Icon == null)
            {
                Type = ActionTypes.Label;
            }

            await base.SetParametersAsync(parameters);
        }
    }
}
