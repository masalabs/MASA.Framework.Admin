using FluentValidation;
using MASA.Contrib.ReadWriteSpliting.CQRS.Commands;
using MASA.Framework.Admin.Contracts.Blogs.BlogType.Options;

namespace MASA.Framework.Admin.Service.Blogs.Application.Commands
{

    public class CreateBlogTypeCommandValidator : AbstractValidator<CreateBlogTypeCommand>
    {
        public CreateBlogTypeCommandValidator()
        {
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.CreationTime).GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5)).WithMessage("abnormal payment time");
            RuleFor(cmd => cmd.CreationTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("2 abnormal payment time");


            RuleFor(cmd => cmd.Request).NotNull().WithMessage("Request is not null");
            RuleFor(cmd => cmd.Request.TypeName).NotEqual(default(string)).WithMessage("TypeName is not null");
        }
    }

    public record class CreateBlogTypeCommand : Command
    {
        public CreateBlogTypeCommand()
        {
        }

        public CreateBlogTypeCommand(CreateBlogTypeModel request)
        {
            this.Request = request;
        }

        public CreateBlogTypeModel Request { get; set; }
    }
}
