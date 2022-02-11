namespace MASA.Framework.Admin.Service.Blogs.Application.BlogAdvertisingPictures.Querys
{
    public record class GetBlogAdvertisingPicturesQuery : Contrib.ReadWriteSpliting.CQRS.Queries.Query<
        PagingResult<BlogAdvertisingPicturesListViewModel>>
    {
        public GetBlogAdvertisingPicturesQuery()
        {
        }

        public GetBlogAdvertisingPicturesQuery(GetBlogAdvertisingPicturesOption request)
        {
            this.Request = request;
        }

        public GetBlogAdvertisingPicturesOption Request { get; set; }

        public override PagingResult<BlogAdvertisingPicturesListViewModel> Result { get; set; }
    }
}