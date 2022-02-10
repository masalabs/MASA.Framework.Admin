using FluentValidation;
using MASA.Contrib.ReadWriteSpliting.CQRS.Commands;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Commands
{
    public class UpdateBlogTypeCommandValidator : AbstractValidator<UpdateBlogTypeCommand>
    {
        public UpdateBlogTypeCommandValidator()
        {
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.CreationTime).GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5)).WithMessage("abnormal payment time");
            RuleFor(cmd => cmd.CreationTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("2 abnormal payment time");


            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("Id is not null");
            RuleFor(cmd => cmd.TypeName).NotEqual(default(string)).WithMessage("TypeName is not null");
        }
    }

    public record class UpdateBlogTypeCommand : Command
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
    }



}
