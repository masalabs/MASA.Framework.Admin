namespace MASA.Framework.Admin.Service.Blogs.Application.BlogAdvertisingPictures.Commands
{
    public class UpdateBlogAdvertisingPicturesCommandValidator : AbstractValidator<UpdateBlogAdvertisingPicturesCommand>
    {
        public UpdateBlogAdvertisingPicturesCommandValidator()
        {
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.CreationTime).GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5)).WithMessage("abnormal payment time");
            RuleFor(cmd => cmd.CreationTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("2 abnormal payment time");


            RuleFor(cmd => cmd.Request).NotNull().WithMessage("Request is not null");
            RuleFor(cmd => cmd.Request.Title).NotEqual(default(string)).WithMessage("Title is not null");
            RuleFor(cmd => cmd.Request.Sort).NotEqual(default(short)).WithMessage("Sort is not default");
            RuleFor(cmd => cmd.Request.Type).NotEqual(default(short)).WithMessage("Type is not default");
        }
    }

    public record class UpdateBlogAdvertisingPicturesCommand : Command
    {
        public UpdateBlogAdvertisingPicturesCommand()
        {

        }

        public UpdateBlogAdvertisingPicturesCommand(UpdateBlogAdvertisingPicturesModel request)
        {

        }

        public UpdateBlogAdvertisingPicturesModel Request { get; set; }
    }
}
