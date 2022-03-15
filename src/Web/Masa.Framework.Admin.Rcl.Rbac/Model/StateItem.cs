namespace Masa.Framework.Admin.Rcl.Rbac.Model;

public class StateItem
{
    public bool Value { get; set; }
    public string Text { get; set; }

    public StateItem(bool value, string text)
    {
        Value = value;
        Text = text;
    }
}

