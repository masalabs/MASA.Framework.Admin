using OneOf;

namespace MASA.Framework.Admin.Management.Components;

/// <inheritdoc />
public partial class DefaultDataTable<TItem> : MDataTable<TItem>
{
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        FooterProps = new Dictionary<string, object>()
        {
            {
                "ItemsPerPageOptions", new List<OneOf<int, DataItemsPerPageOption>>() { 5, 10, 15 }
            }
        };
    }
}