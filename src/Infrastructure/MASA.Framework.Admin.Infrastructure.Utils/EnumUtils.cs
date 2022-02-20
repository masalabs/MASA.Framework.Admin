namespace MASA.Framework.Admin.Utils
{
    public static class EnumUtils
    {
        public static string GetDescription(this Enum value)
        {
            return GetCustomerObj<DescriptionAttribute>(value)?.Description ?? string.Empty;
        }

        public static T? GetCustomerObj<T>(this Enum value) where T : Attribute
        {
            string name = value.ToString();
            if (!string.IsNullOrEmpty(name))
            {
                // 获取枚举字段。
                FieldInfo? fieldInfo = value.GetType().GetField(name);
                if (fieldInfo != null)
                {
                    // 获取描述的属性。
                    if (Attribute.GetCustomAttribute(fieldInfo,
                            typeof(T), false) is T attr)
                    {
                        return attr;
                    }
                }
            }
            return null;
        }
    }
}
