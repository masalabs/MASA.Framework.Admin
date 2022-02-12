namespace MASA.Framework.Admin.Blog.Components
{
    public class DefaultAutoComplete<TItem, TItemValue, TValue> : MAutocomplete<TItem, TItemValue, TValue>
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
}
