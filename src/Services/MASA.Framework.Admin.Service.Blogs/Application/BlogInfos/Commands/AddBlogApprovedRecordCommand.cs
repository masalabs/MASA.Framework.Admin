namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Commands
{
    public class AddBlogApprovedRecordValidator : AbstractValidator<AddBlogApprovedRecordCommand>
    {
        public AddBlogApprovedRecordValidator()
        {
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.CreationTime).GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5)).WithMessage("abnormal payment time");
            RuleFor(cmd => cmd.CreationTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("2 abnormal payment time");


            RuleFor(cmd => cmd.Request).NotNull().WithMessage("Request is not null");
        }
    }

    public record class AddBlogApprovedRecordCommand : Command
    {
        public AddBlogApprovedRecordCommand()
        {

        }

        public AddBlogApprovedRecordCommand(BlogApprovedRecordModel request)
        {
            this.Request = request;
        }

        public BlogApprovedRecordModel Request { get; set; }
    }

  
}
