using OneOf;

namespace MASA.Framework.Admin.Blog.Components;

/// <inheritdoc />
public partial class DefaultDataTable<TItem> : MDataTable<TItem>
{
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        FooterProps = new Dictionary<string, object>()
        {
            {
                "ItemsPerPageOptions", new List<OneOf<int, DataItemsPerPageOption>>() { 5, 10, 15 }
            }
        };

        await base.SetParametersAsync(parameters);
    }
}