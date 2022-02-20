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
                    _ = component.PopupService.MessageAsync(context.Exception);
                }
                else
                {
                    //component.NavigateTo("Error", true);
                    _ = component.PopupService.MessageAsync(context.Exception);
                }

                context.ExceptionHandled = true;
            }
        }
    }
}
