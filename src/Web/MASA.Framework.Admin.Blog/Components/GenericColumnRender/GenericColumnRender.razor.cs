using Microsoft.AspNetCore.Components;

namespace MASA.Framework.Admin.Blog.Components;

public partial class GenericColumnRender
{
    [Parameter]
    public Func<bool, string> BoolRender { get; set; }

    [Parameter]
    public bool ChippedEnum { get; set; }

    [Parameter]
    public bool SmallChip { get; set; }

    [Parameter]
    public object Value { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        BoolRender ??= b => b ? "是" : "否";
    }
}