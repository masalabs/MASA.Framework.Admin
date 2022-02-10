namespace MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Querys
{
    public record class GetBlogTypePagingQuery : Contrib.ReadWriteSpliting.CQRS.Queries.Query<PageResult<BlogTypePagingViewModel>>
    {
        public GetBlogTypePagingQuery()
        {

        }

        public GetBlogTypePagingQuery(GetBlogTypePagingOption request)
        {
            this.Request = request;
        }

        public GetBlogTypePagingOption Request { get; set; }

        public override PageResult<BlogTypePagingViewModel> Result { get; set; }
    }
}
