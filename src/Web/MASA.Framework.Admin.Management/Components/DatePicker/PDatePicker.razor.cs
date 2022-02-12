namespace MASA.Framework.Admin.Management.Components;

public partial class PDatePicker<TValue>
{
    #region MTextField Parameters

    [Parameter] public bool Clearable { get; set; }
    [Parameter] public bool Dense { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public StringBoolean HideDetails { get; set; }
    [Parameter] public string Label { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnClearClick { get; set; }
    [Parameter] public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }
    [Parameter] public bool Outlined { get; set; }
    [Parameter] public string PrependIcon { get; set; }
    [Parameter] public string PrependInnerIcon { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = new();

    #endregion

    #region MDatePicker Parameters

    [Parameter] public bool NoTitle { get; set; }
    [Parameter] public bool Range { get; set; }
    [Parameter] public bool Scrollable { get; set; }
    [Parameter] public TValue Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }

    #endregion

    [Parameter] public DateOnly? DefaultSelectedValue { get; set; }
    [Parameter] public string Format { get; set; }
    [Parameter] public EventCallback OnOk { get; set; }

    private bool _menuValue;

    private TValue InternalValue { get; set; }

    private string TextFieldValue
    {
        get
        {
            return Value switch
            {
                DateOnly date when date != DateOnly.MinValue => date.ToString(Format),
                // TODO: DateOnly.MinValue in dates?
                IList<DateOnly> dates => string.Join(" ~ ", dates.Select(date => date.ToString(Format))),
                _ => string.Empty
            };
        }
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        PrependInnerIcon = "mdi-calendar";
    }

    private void HandleOnCancel()
    {
        _menuValue = false;
    }

    private async Task HandleOnClearClick()
    {
        await UpdateValue(default);

        await OnClearClick.InvokeAsync();
    }

    private async Task HandleOnOk()
    {
        await UpdateValue(InternalValue);

        await OnOk.InvokeAsync();

        _menuValue = false;
    }

    private void InternalValueChanged(TValue value)
    {
        InternalValue = value;
    }

    private void MenuValueChanged(bool value)
    {
        if (value)
        {
            InternalValue = Value;

            if (InternalValue == null || InternalValue.Equals(default))
            {
                // Apply the DefaultSelectedValue to Range DatePicker may be confusing,
                // so ignore that
                UpdateInternalValue(DefaultSelectedValue, true);
            }
        }

        _menuValue = value;
    }

    private void UpdateInternalValue(DateOnly? dateOnly, bool ignoreRange = false)
    {
        if (!dateOnly.HasValue)
        {
            return;
        }

        if (Range)
        {
            if (ignoreRange) return;
            
            InternalValue = (TValue)(object)new List<DateOnly>() { dateOnly.Value };
        }
        else
        {
            InternalValue = (TValue)(object)dateOnly.Value;
        }
    }

    private async Task UpdateValue(TValue value)
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(value);
        }
        else
        {
            Value = value;
        }
    }
}