namespace MASA.Framework.Admin.Management.Helpers;

public static class ColorHelper
{
    public readonly static string[] Colors =
    {
        "pink lighten-5",
        "purple lighten-5",
        "blue lighten-5",
        "cyan lighten-5",
        "green lighten-5",
        "lime lighten-5",
        "amber lighten-5",
        "red lighten-5",
        "indigo lighten-5",
        "light-blue lighten-5",
        "teal lighten-5",
        "yellow lighten-5",
        "orange lighten-5",
        "brown lighten-5",
        "blue-grey lighten-5",
        "grey lighten-5",
    };

    public static string GenChipColorClass(int index)
    {
        if (index > Colors.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        var color = index == -1 ? Colors.Last() : Colors[index];

        var textColor = color.Split(" ")[0] + "--text";

        return $"{color} {textColor}";
    }

    public static string GenChipColorClass(string color)
    {
        ArgumentNullException.ThrowIfNull(color);

        return $"{color} lighten-5 {color}--text";
    }

    public static (string color, string variant) GetColor(int index)
    {
        if (index > Colors.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        var result = Colors[index].Split(" ");

        return (result[0], result[1]);
    }
}