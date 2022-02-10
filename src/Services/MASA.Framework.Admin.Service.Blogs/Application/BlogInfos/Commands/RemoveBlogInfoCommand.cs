using FluentValidation;
using MASA.Contrib.ReadWriteSpliting.CQRS.Commands;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Commands
{
    public class RemoveBolgInfoCommandValidator : AbstractValidator<RemoveBlogInfoCommand>
    {
        public RemoveBolgInfoCommandValidator()
        {
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.CreationTime).GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5)).WithMessage("abnormal payment time");
            RuleFor(cmd => cmd.CreationTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("2 abnormal payment time");


            RuleFor(cmd => cmd.Ids).NotNull().WithMessage("Ids is not null");
        }
    }


    public record class RemoveBlogInfoCommand : Command
    {
        public RemoveBlogInfoCommand()
        {

        }

        public RemoveBlogInfoCommand(Guid[] ids)
        {
            this.Ids = ids;
        }

        public Guid[] Ids { get; set; }
    }
}
