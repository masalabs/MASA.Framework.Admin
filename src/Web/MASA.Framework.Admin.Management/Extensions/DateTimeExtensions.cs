namespace System;

public static class DateTimeExtensions
{
    public static bool IsDefault(this DateTime dateTime)
    {
        if (dateTime == DateTime.MinValue)
        {
            return true;
        }

        var businessDefaultValue = new DateTime(1900, 1, 1);

        return dateTime <= businessDefaultValue;
    }

    /// <summary>
    /// 时间格式化
    /// </summary>
    /// <param name="dateTime">时间</param>
    /// <param name="hasTime">格式化是否保留时分秒</param>
    /// <param name="addHours">追加的时区时间</param>
    /// <returns></returns>
    public static string ToDateTimeFormat(this DateTime dateTime, 
        bool hasTime = true, int addHours = 8)
    {
        if (dateTime.IsDefault())
        {
            return string.Empty;
        }
        
        var utcTime = dateTime.ToUniversalTime();
        dateTime = utcTime.AddHours(addHours);

        return hasTime ? 
            dateTime.ToString("yyyy-MM-dd HH:mm:ss") : 
            dateTime.ToString("yyyy-MM-dd");
    }
}