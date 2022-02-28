namespace Masa.Framework.Admin.Infrastructure.Utils;

public class RandomUtils
{
    private const string LETTERS = "ABCDEFGHIJKMLNOPQRSTUVWXYZabcdefghigklmnopqrstuvwxyz";

    private const string NUMBERS = "0123456789";

    private static readonly Random Random;

    static RandomUtils()
    {
        Random = new Random();
    }

    public static string GenerateSpecifiedString(int length, string? text = null)
    {
        if (text == null)
            text = LETTERS + NUMBERS;
        var result = new StringBuilder();
        for (int i = 0; i < length; i++)
            result.Append(GetRandomChar(text));
        return result.ToString();
    }

    private static string GetRandomChar(string text) => text[Random.Next(1, text.Length)].ToString();
}
