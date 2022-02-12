namespace MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Querys
{
    public record class GetBlogTypeCondensedQuery : Contrib.ReadWriteSpliting.CQRS.Queries.Query<List<BlogTypeCondensedViewModel>>
    {
        public override List<BlogTypeCondensedViewModel> Result { get; set; } = default!;
    }
}
