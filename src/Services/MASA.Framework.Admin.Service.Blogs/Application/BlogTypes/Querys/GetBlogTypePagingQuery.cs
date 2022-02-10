using FluentValidation;
using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
using MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Commands;
using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Admin.Service.Blogs.Model.BlogType.Options;
using MASA.Framework.Admin.Service.Blogs.Model.BlogType.Options.ViewModel;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Querys
{
    public class GetBlogTypePagingQueryValidator : AbstractValidator<GetBlogTypePagingQuery>
    {
        public GetBlogTypePagingQueryValidator()
        {
            RuleFor(cmd => cmd.Id).NotEqual(default(Guid)).WithMessage("wrong id");
            RuleFor(cmd => cmd.CreationTime).GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(-5)).WithMessage("abnormal payment time");
            RuleFor(cmd => cmd.CreationTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("2 abnormal payment time");


            RuleFor(cmd => cmd.Request).NotNull().WithMessage("Request is not null");
        }
    }

    public record class GetBlogTypePagingQuery : Query<BlogTypePagingViewModel>
    {
        public GetBlogTypePagingOption Request { get; set; }

        public override BlogTypePagingViewModel Result { get; set; }
    }
}
