namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Commands
{
    public record class WithdrawBlogArticleCommand : Command
    {
        public WithdrawBlogArticleModel Model { get; set; }

        public WithdrawBlogArticleCommand()
        {
        }

        public WithdrawBlogArticleCommand(WithdrawBlogArticleModel model)
        {
            Model = model;
        }
    }

    public class WithdrawBlogArticleCommandValidator : AbstractValidator<WithdrawBlogArticleCommand>
    {
        public WithdrawBlogArticleCommandValidator(IBlogArticleRepository blogArticleRepository)
        {
            RuleFor(u => u.Model.Id)
                .NotNull()
                .NotEqual(Guid.Empty)
                .MustAsync((id, _) => blogArticleRepository.ExistAsync(id)).WithMessage("文章不存在");

            RuleFor(u => u.Model.ReasonType).NotEqual((ReasonTypes)0);
        }
    }
}
