﻿namespace MASA.Framework.Admin.Blog.Components
{
    public class DefaultAutoComplete<TItem, TItemValue, TValue> : MAutocomplete<TItem, TItemValue, TValue>
    {
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            Dense = true;
            HideDetails = "auto";
            Outlined = true;
            Clearable = true;

            await base.SetParametersAsync(parameters);
        }
    }
}
