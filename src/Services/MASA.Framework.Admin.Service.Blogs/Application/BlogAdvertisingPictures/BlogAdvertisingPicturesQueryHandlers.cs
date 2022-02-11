namespace MASA.Framework.Admin.Service.Blogs.Application.BlogAdvertisingPictures
{
    public class BlogAdvertisingPicturesQueryHandlers
    {
        private readonly IBlogAdvertisingPicturesRepository _advertisingPicturesRepository;

        public BlogAdvertisingPicturesQueryHandlers(IBlogAdvertisingPicturesRepository advertisingPicturesRepository)
        {
            this._advertisingPicturesRepository = advertisingPicturesRepository;
        }

        [EventHandler]
        public async Task GetListAsync(GetBlogAdvertisingPicturesQuery query)
        {
            var list = await this._advertisingPicturesRepository.GetListAsync(query.Request);

            query.Result = list;
        }

        [EventHandler]
        public async Task GetBlogFrontListAsync(GetBlogAdvertisingPicturesFrontQuery query)
        {
            var list = await this._advertisingPicturesRepository.GetBlogFrontListAsync(query.Request);

            query.Result = list;
        }
    }
}
