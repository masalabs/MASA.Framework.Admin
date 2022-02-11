namespace MASA.Framework.Admin.Service.Blogs.Application.BlogComments.Commands
{
    public class RemoveBlogCommentValidator : AbstractValidator<RemoveBlogCommentCommand>
    {
        public RemoveBlogCommentValidator()
        {
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.CreationTime).GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5)).WithMessage("abnormal payment time");
            RuleFor(cmd => cmd.CreationTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("2 abnormal payment time");


            RuleFor(cmd => cmd.Request).NotNull().WithMessage("Request is not null");
            RuleFor(cmd => cmd.Request.Id).NotEqual(default(Guid)).WithMessage("wrong id");
        }
    }

    public record class RemoveBlogCommentCommand : Command
    {
        public RemoveBlogCommentCommand()
        {

        }

        public RemoveBlogCommentCommand(RemoveCommentModel request)
        {
            Request = request;
        }

        public RemoveCommentModel Request { get; set; }
    }
}
