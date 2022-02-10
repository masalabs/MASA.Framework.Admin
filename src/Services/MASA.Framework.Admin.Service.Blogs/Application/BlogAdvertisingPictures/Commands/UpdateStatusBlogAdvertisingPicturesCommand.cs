namespace MASA.Framework.Admin.Service.Blogs.Application.BlogAdvertisingPictures.Commands
{

    public class UpdateStatusBlogAdvertisingPicturesCommandValidator : AbstractValidator<UpdateStatusBlogAdvertisingPicturesCommand>
    {
        public UpdateStatusBlogAdvertisingPicturesCommandValidator()
        {
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.CreationTime).GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5)).WithMessage("abnormal payment time");
            RuleFor(cmd => cmd.CreationTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("2 abnormal payment time");


            RuleFor(cmd => cmd.Request).NotNull().WithMessage("Request is not null");
            RuleFor(cmd => cmd.Request.Id).NotEqual(default(Guid)).WithMessage("Title is not null");
        }
    }

    public record class UpdateStatusBlogAdvertisingPicturesCommand : Command
    {
        public UpdateStatusBlogAdvertisingPicturesCommand()
        {

        }

        public UpdateStatusBlogAdvertisingPicturesCommand(UpdateStatusBlogAdvertisingPicturesModel request)
        {

        }

        public UpdateStatusBlogAdvertisingPicturesModel Request { get; set; }

    }
}
