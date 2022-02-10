using FluentValidation;
using MASA.Contrib.ReadWriteSpliting.CQRS.Commands;
using MASA.Framework.Admin.Service.Blogs.Model.BlogType.Options;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Commands
{
    public class UpdateBlogTypeCommandValidator : AbstractValidator<UpdateBlogTypeCommand>
    {
        public UpdateBlogTypeCommandValidator()
        {
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.CreationTime).GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5)).WithMessage("abnormal payment time");
            RuleFor(cmd => cmd.CreationTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("2 abnormal payment time");


            RuleFor(cmd => cmd.Request).NotNull().WithMessage("Request is not null");
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("Id is not null");
            RuleFor(cmd => cmd.Request.TypeName).NotEqual(default(string)).WithMessage("TypeName is not null");
        }
    }

    public record class UpdateBlogTypeCommand : Command
    {

        public UpdateBlogTypeCommand()
        {

        }

        public UpdateBlogTypeCommand(UpdateBlogTypeModel request)
        {
            this.Request = request;
        }

        public UpdateBlogTypeModel Request { get; set; }

    }



}
