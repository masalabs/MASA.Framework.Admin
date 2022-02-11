namespace MASA.Framework.Admin.Service.Blogs.Application.BlogComments.Commands
{
    public class CreateBlogCommentValidator : AbstractValidator<CreateBlogCommentCommand>
    {
        public CreateBlogCommentValidator()
        {
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.CreationTime).GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5)).WithMessage("abnormal payment time");
            RuleFor(cmd => cmd.CreationTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("2 abnormal payment time");


            RuleFor(cmd => cmd.Request).NotNull().WithMessage("Request is not null");
            RuleFor(cmd => cmd.Request.CommentContent).NotEqual(default(string)).WithMessage("CommentContent is not null");
            RuleFor(cmd => cmd.Request.BlogInfoId).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.Request.TypeId).NotEqual(default(Guid)).WithMessage("wrong TypeId");
            RuleFor(cmd => cmd.Request.CreatorUserId).NotEqual(default(Guid)).WithMessage("wrong CreatorUserId");
        }
    }

    public record class CreateBlogCommentCommand : Command
    {
        public CreateBlogCommentCommand()
        {

        }

        public CreateBlogCommentCommand(AddCommentModel request)
        {
            Request = request;
        }

        public AddCommentModel Request { get; set; }
    }
}
