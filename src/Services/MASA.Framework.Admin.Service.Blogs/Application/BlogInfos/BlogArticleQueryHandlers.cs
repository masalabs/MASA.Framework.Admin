namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos
{
    public class BlogArticleQueryHandlers
    {
        private string _defaultIndex;
        private readonly ElasticClient _elasticClient;
        private ILogger<BlogArticleCommandHandler> _logger;
        private readonly IBlogArticleRepository _blogArticleRepository;
        private readonly IBlogLabelRepository _blogLabelRepository;

        public BlogArticleQueryHandlers(
            IOptions<BlogAppSettiings> settings,
            IBlogLabelRepository blogLabelRepository,
            IBlogArticleRepository blogArticleRepository,
            ILogger<BlogArticleCommandHandler> logger,
            IElasticClientProvider elasticClientProvider)
        {
            _logger = logger;
            _blogLabelRepository = blogLabelRepository;
            _blogArticleRepository = blogArticleRepository;
            _defaultIndex = $"{settings.Value.ElasticConfig.IndexPrefix}_{nameof(BlogInfo)}".ToLower();
            _elasticClient = elasticClientProvider.GetClient(_defaultIndex);
        }

        [EventHandler]
        public async Task BlogArticleQueryAsync(BlogArticleQuery query)
        {
            var blogArticle = await _blogArticleRepository.GetListAsync(query.Options);

            query.Result = blogArticle;
        }

        [EventHandler]
        public async Task BlogArticleDetailsQueryAsync(BlogArticleDetailsQuery query)
        {
            var blogArticle = await _blogArticleRepository.GetAsync(query.Id);
            if (blogArticle is not null)
                blogArticle.Relations = await _blogLabelRepository.GetRelationsByBlog(blogArticle.id);

            query.Result = blogArticle;
        }

        [EventHandler]
        public async Task BlogArticleUserAsync(BlogArticleUserQuery query)
        {
            var blogArticle = await _blogArticleRepository.GetBlogArticleByUser(query.Options);

            query.Result = blogArticle; 
        }

        [EventHandler]
        public async Task BlogArticleHomeAsync(BlogArticleHomeQuery query)
        {
            List<Func<QueryContainerDescriptor<BlogInfo>, QueryContainer>> matchQuery = new()
            {
                mu => mu.Term("state", ((int)StateTypes.Reviewed).ToString()),
                match => match.Match(m => m.Field(fd => fd.IsDeleted).Lenient(false))
            };

            if (!string.IsNullOrWhiteSpace(query.Options.KeyWords))
            {
                matchQuery.Add(mu => mu.Match(m => m.Field(fd => fd.Title.Contains(query.Options.KeyWords))));
                matchQuery.Add(mu => mu.Match(m => m.Field(fd => fd.Content.Contains(query.Options.KeyWords))));
            }

            if (query.Options.TypeId.HasValue)
            {
                matchQuery.Add(mu => mu.Term("typeId", query.Options.TypeId.Value.ToString()));
            }

            var searchResponse = await _elasticClient.SearchAsync<BlogInfo>(w => w
                .Index(_defaultIndex)
                .From(query.Options.PageIndex <= 1 ? 0 : (query.Options.PageIndex - 1) * query.Options.PageSize)
                .Size(query.Options.PageSize)
                .Query(q => q.Bool(b => b.Must(matchQuery)))
                .Sort(s => s.Descending(y => y.CreationTime)));

            var data = new Mapping<BlogInfoHomeListViewModel, BlogInfo>().ReverseMap(searchResponse.Documents).ToList();

            query.Result = new()
            {
                Data = data,
                Page = query.Options.PageIndex,
                Size = query.Options.PageSize,
                TotalCount = (int)searchResponse.Total
            };
        }
    }
}
