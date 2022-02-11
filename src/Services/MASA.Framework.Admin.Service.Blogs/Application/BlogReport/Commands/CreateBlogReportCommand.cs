namespace MASA.Framework.Admin.Service.Blogs.Application.BlogReport.Commands
{
    public class CreateBlogReportCommandValidator : AbstractValidator<CreateBlogReportCommand>
    {
        public CreateBlogReportCommandValidator()
        {
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.CreationTime).GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5)).WithMessage("abnormal payment time");
            RuleFor(cmd => cmd.CreationTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("2 abnormal payment time");

            RuleFor(cmd => cmd.Request).NotNull().WithMessage("Request is not null");
            RuleFor(cmd => cmd.Request.Title).NotEqual(default(string)).WithMessage("Title is not null");
            RuleFor(cmd => cmd.Request.Connect).NotEqual(default(string)).WithMessage("Connect is not null");
            RuleFor(cmd => cmd.Request.Detail).NotEqual(default(string)).WithMessage("Detail is not null");
            RuleFor(cmd => cmd.Request.Reason).NotEqual(default(ReasonTypes)).WithMessage("Reason is not null");
            RuleFor(cmd => cmd.Request.BlogInfoId).NotEqual(default(Guid)).WithMessage("BlogInfoId is not null");
        }
    }

    public record class CreateBlogReportCommand : Command
    {
        public CreateBlogReportCommand()
        {

        }

        public CreateBlogReportCommand(CreateBlogReportModel request)
        {
            Request = request;
        }

        public CreateBlogReportModel Request { get; set; }

    }
}
