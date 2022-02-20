namespace MASA.Framework.Admin.Blog.Components
{
    public class DefaultGenericColumnRender : GenericColumnRender
    {
        [CascadingParameter(Name = "TimeZoneOffset")]
        protected TimeSpan TimeZoneOffset { get; set; }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            DateFormat = "yyyy-MM-dd";
            TimeFormat = "HH:mm:ss";

            if (Value is DateTime dateTime && !dateTime.IsDefault())
            {
                InternalValue = dateTime.Add(TimeZoneOffset);
            }

            await base.SetParametersAsync(parameters);
        }
    }
}
