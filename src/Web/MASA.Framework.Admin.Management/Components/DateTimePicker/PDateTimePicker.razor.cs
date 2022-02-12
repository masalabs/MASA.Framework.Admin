namespace MASA.Framework.Admin.Management.Components;

public partial class PDateTimePicker<TValue>
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
    [Parameter] public TValue Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }

    #endregion

    [Parameter] public DateTime? DefaultSelectedValue { get; set; }
    [Parameter] public string Format { get; set; }
    [Parameter] public EventCallback OnOk { get; set; }

    private bool _menuValue;
    private bool _hourFocused;
    private bool _minuteFocused;
    private bool _secondFocused;

    private DateTime? InternalValue { get; set; }

    private string TextFieldValue
    {
        get
        {
            return Value switch
            {
                DateTime dateTime when dateTime != DateTime.MinValue => dateTime.ToString(Format),
                _ => string.Empty
            };
        }
    }

    private bool TimeFocused => _hourFocused || _minuteFocused || _secondFocused;

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
        await UpdateValue(null);

        await OnClearClick.InvokeAsync();
    }

    private async Task HandleOnOk()
    {
        await UpdateValue(InternalValue);

        await OnOk.InvokeAsync();

        _menuValue = false;
    }

    private void InternalValueChanged(DateTime? value)
    {
        InternalValue = value;
    }

    private void MenuValueChanged(bool value)
    {
        if (value)
        {
            if (Value is DateTime dateTime && dateTime != DateTime.MinValue)
            {
                InternalValue = dateTime;
            }
            else
            {
                InternalValue = null;
            }

            InternalValue ??= DefaultSelectedValue;
        }

        _menuValue = value;
    }

    private void OnHourFocus() => _hourFocused = true;

    private void OnHourBlur() => _hourFocused = false;

    private void OnMinuteFocus() => _minuteFocused = true;

    private void OnMinuteBlur() => _minuteFocused = false;

    private void OnSecondFocus() => _secondFocused = true;

    private void OnSecondBlur() => _secondFocused = false;

    private void UpdateInternalValue(DateTime? dateTime)
    {
        InternalValue = dateTime;
    }

    private async Task UpdateValue(DateTime? value)
    {
        var v = default(TValue);

        if (value.HasValue)
        {
            v = (TValue)(object)value;
        }

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(v);
        }
        else
        {
            Value = v;
        }
    }
}