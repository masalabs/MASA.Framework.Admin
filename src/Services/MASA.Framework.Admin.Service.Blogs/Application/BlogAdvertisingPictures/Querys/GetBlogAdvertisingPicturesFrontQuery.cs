namespace MASA.Framework.Admin.Service.Blogs.Application.BlogAdvertisingPictures.Querys
{
    public record class GetBlogAdvertisingPicturesFrontQuery : 
        Contrib.ReadWriteSpliting.CQRS.Queries.Query<List<BlogAdvertisingPicturesListViewModel>>
    {
        public GetBlogAdvertisingPicturesFrontQuery()
        {

        }

        public GetBlogAdvertisingPicturesFrontQuery(GetBlogAdvertisingPicturesFrontOption request)
        {
            this.Request = request;
        }

        public GetBlogAdvertisingPicturesFrontOption Request { get; set; }

        public override List<BlogAdvertisingPicturesListViewModel> Result { get; set; }
    }
}
