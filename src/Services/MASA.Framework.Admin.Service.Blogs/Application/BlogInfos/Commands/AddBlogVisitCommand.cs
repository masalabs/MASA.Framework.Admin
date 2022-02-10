namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Commands
{
    public class AddBlogVisitValidator : AbstractValidator<AddBlogVisitCommand>
    {
        public AddBlogVisitValidator()
        {
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.CreationTime).GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5)).WithMessage("abnormal payment time");
            RuleFor(cmd => cmd.CreationTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("2 abnormal payment time");


            RuleFor(cmd => cmd.Request).NotNull().WithMessage("Request is not null");
            RuleFor(cmd => cmd.Request.UserId).NotEqual(default(Guid)).WithMessage("wrong UserId");
        }
    }

    public record class AddBlogVisitCommand : Command
    {
        public AddBlogVisitCommand()
        {

        }

        public AddBlogVisitCommand(AddBlogVisitModel request)
        {
            this.Request = request;
        }

        public AddBlogVisitModel Request { get; set; }
    }

  
}
