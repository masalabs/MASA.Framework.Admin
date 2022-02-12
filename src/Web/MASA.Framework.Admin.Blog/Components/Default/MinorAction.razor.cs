namespace MASA.Framework.Admin.Blog.Components
{
    public class MinorAction : PAction
    {
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            Color = "primary";
            Text = true;
            
            if (Icon == null)
            {
                Type = ActionTypes.Label;
            }
            
            await base.SetParametersAsync(parameters);
        }
    }
}
