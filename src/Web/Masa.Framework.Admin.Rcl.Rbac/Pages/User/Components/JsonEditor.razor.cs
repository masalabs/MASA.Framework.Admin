using Microsoft.JSInterop;
using System.Text.RegularExpressions;

namespace Masa.Framework.Admin.Rcl.Rbac.Pages.User.Components;

public partial class JsonEditor
{
    private int _lineCount = 1;
    private int _errorLine;
    private string _error;

    public ElementReference Ref { get; set; }

    [Inject]
    public IJSRuntime Js { get; set; }

    [Parameter]
    public string Value { get; set; } = "";

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public EventCallback<ChangeEventArgs> HandleOnInput { get; set; }

    protected override void OnParametersSet()
    {
        if (!string.IsNullOrEmpty(Value))
        {
            _lineCount = Regex.Matches(Value, "\n").Count + 1;
        }
    }

    public async void HandleOnChange(ChangeEventArgs args)
    {
        Value = args.Value.ToString();
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    public async void OnInput(ChangeEventArgs args)
    {
        if (HandleOnInput.HasDelegate)
        {
            await HandleOnInput.InvokeAsync(args);
        }

        var value = args.Value.ToString();
        _lineCount = Regex.Matches(value, "\n").Count + 1;

        try
        {
            //var obj = JObject.Parse(value);
            _errorLine = 0;
        }
        catch (Exception ex)
        {
            _error = ex.Message;

            var match = Regex.Match(ex.Message, "line (?<line>[0-9]*),");
            if (match.Success)
            {
                _errorLine = int.Parse(match.Groups["line"].Value) - 1;
                if (_errorLine == 0)
                {
                    _errorLine = 1;
                }
            }
        }
    }
}


