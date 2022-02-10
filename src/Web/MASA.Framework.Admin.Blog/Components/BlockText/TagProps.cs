namespace MASA.Framework.Admin.Blog.Components;

public class TagProps
{
    public string Text { get; set; }

    public string Color { get; set; }
    
    public string Class { get; set; }

    public TagProps()
    {
    }

    public TagProps(string text, string color)
    {
        Text = text;
        Color = color;
    }
    
    public TagProps(string text, string color, string @class)
    {
        Text = text;
        Color = color;
        Class = @class;
    }
}