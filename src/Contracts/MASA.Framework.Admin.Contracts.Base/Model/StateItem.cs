namespace MASA.Framework.Admin.Contracts.Base.Model;

public class StateItem
{
    public int Value { get; set; }
    public string Text { get; set; }

    public StateItem(int value, string text)
    {
        Value = value;
        Text = text;
    }
}
