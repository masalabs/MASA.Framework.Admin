namespace MASA.Framework.Admin.Blog.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Component is ProCompontentBase component)
            {
                if (context.Exception is UserFriendlyException)
                {
                    component.Message(context.Exception.Message, AlertTypes.Error);
                }
                else
                {
                    //component.NavigateTo("Error", true);
                    component.Message(context.Exception.Message, AlertTypes.Error);
                }

                context.ExceptionHandled = true;
            }
        }
    }
}
