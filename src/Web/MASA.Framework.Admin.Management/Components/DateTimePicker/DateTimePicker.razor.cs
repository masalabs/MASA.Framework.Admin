namespace MASA.Framework.Admin.Management.Components;

public partial class DateTimePicker<TValue>
{
    #region MDatePicker Parameters

    [Parameter] public bool NoTitle { get; set; }
    [Parameter] public TValue Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    #endregion

    [Parameter] public EventCallback<FocusEventArgs> OnHourFocus { get; set; }
    [Parameter] public EventCallback<FocusEventArgs> OnHourBlur { get; set; }
    [Parameter] public EventCallback<FocusEventArgs> OnMinuteFocus { get; set; }
    [Parameter] public EventCallback<FocusEventArgs> OnMinuteBlur { get; set; }
    [Parameter] public EventCallback<FocusEventArgs> OnSecondFocus { get; set; }
    [Parameter] public EventCallback<FocusEventArgs> OnSecondBlur { get; set; }

    private static readonly int[] Hours = Enumerable.Range(0, 24).ToArray();
    private static readonly int[] Minutes = Enumerable.Range(0, 60).ToArray();
    private static readonly int[] Seconds = Enumerable.Range(0, 60).ToArray();

    private DateOnly? Date { get; set; }
    private TimeOnly Time { get; set; }
    private int Hour { get; set; }
    private int Minute { get; set; }
    private int Second { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Value is DateTime value && value != DateTime.MinValue)
        {
            Date = DateOnly.FromDateTime(value);
            Time = TimeOnly.FromDateTime(value);
        }
        else
        {
            Date = null;
            Time = TimeOnly.MinValue;
        }

        Hour = Time.Hour;
        Minute = Time.Minute;
        Second = Time.Second;
    }

    private async Task DateChanged(DateOnly? date)
    {
        Date = date;

        await UpdateValue();
    }

    private async Task HourChanged(int hour)
    {
        Hour = hour;

        await UpdateValue();
    }

    private async Task MinuteChanged(int minute)
    {
        Minute = minute;

        await UpdateValue();
    }

    private async Task SecondChanged(int second)
    {
        Second = second;

        await UpdateValue();
    }

    private async Task UpdateValue()
    {
        var dateTime = default(TValue);

        if (Date.HasValue)
        {
            var time = new TimeOnly(Hour, Minute, Second);

            dateTime = (TValue)(object)Date.Value.ToDateTime(time);
        }

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(dateTime);
        }
        else
        {
            Value = dateTime;
        }
    }
}