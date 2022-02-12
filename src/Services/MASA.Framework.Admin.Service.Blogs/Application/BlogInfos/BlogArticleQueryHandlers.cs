using Nest;
using System.Text.RegularExpressions;
using System.Web;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos
{
    public class BlogCommentsQueryHandlers
    {
        private string _defaultIndex;
        private readonly ElasticClient _elasticClient;
        private ILogger<BlogArticleCommandHandler> _logger;
        private readonly IBlogLabelRepository _blogLabelRepository;
        private readonly IBlogArticleRepository _blogArticleRepository;
        private readonly IBlogApprovedRecordRepository _approvedRecordRepository;

        public BlogCommentsQueryHandlers(
            IOptions<BlogAppSettiings> settings,
            IBlogLabelRepository blogLabelRepository,
            ILogger<BlogArticleCommandHandler> logger,
            IBlogArticleRepository blogArticleRepository,
            IElasticClientProvider elasticClientProvider,
            IBlogApprovedRecordRepository approvedRecordRepository)
        {
            _logger = logger;
            _blogLabelRepository = blogLabelRepository;
            _blogArticleRepository = blogArticleRepository;
            _approvedRecordRepository = approvedRecordRepository;
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
            var blogArticle = await _blogArticleRepository.GetInfoAsync(query.Id);
            if (blogArticle is not null)
            {
                blogArticle.Relations = await _blogLabelRepository.GetRelationsByBlog(blogArticle.id);
                if (query.UserId != Guid.Empty)
                    blogArticle.IsApproved = await _approvedRecordRepository.ExistBlogApprovedRecord(query.Id, query.UserId);
            }

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
                matchQuery.Add(mu => mu.Match(m => m.Field(fd => fd.Title == query.Options.KeyWords)));
                matchQuery.Add(mu => mu.Match(m => m.Field(fd => fd.Content.Contains(query.Options.KeyWords))));
            }

            if (query.Options.TypeId.HasValue && !query.Options.TypeId.Equals(Guid.Empty))
            {
                matchQuery.Add(mu => mu.Term("typeId.keyword", query.Options.TypeId.Value.ToString()));
            }

            var searchResponse = await _elasticClient.SearchAsync<BlogInfo>(w => w
                .Index(_defaultIndex)
                .From(query.Options.PageIndex <= 1 ? 0 : (query.Options.PageIndex - 1) * query.Options.PageSize)
                .Size(query.Options.PageSize)
                .Query(q => q.Bool(b => b.Must(matchQuery)))
                .Sort(s => s.Descending(x => x.ApprovedCount).Descending(y => y.CreationTime)));

            var data = searchResponse.Documents.Select(d => new BlogInfoHomeListViewModel()
            {
                Content = RemoveHTML(d.Content),
                Title = d.Title,
                TypeId = d.TypeId,
                CreationTime = d.CreationTime,
                ApprovedCount = d.ApprovedCount,
                CommentCount = d.CommentCount,
                Id = d.Id,
                ReleaseTime = d.ReleaseTime,
                Visits = d.Visits,
                CreatorUserId = d.CreatorUserId
            }).ToList();

            query.Result = new()
            {
                Data = data,
                Page = query.Options.PageIndex,
                Size = query.Options.PageSize,
                TotalCount = (int)searchResponse.Total
            };
        }

        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="htmlString"></param>
        /// <returns></returns>
        private string RemoveHTML(string htmlString)
        {
            //删除脚本
            htmlString = Regex.Replace(htmlString, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            htmlString = Regex.Replace(htmlString, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"-->", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"<!--.*", "", RegexOptions.IgnoreCase);

            htmlString = Regex.Replace(htmlString, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            htmlString.Replace("<", "");
            htmlString.Replace(">", "");
            htmlString.Replace("\r\n", "");
            htmlString = HttpUtility.HtmlEncode(htmlString).Trim();

            return htmlString;
        }
    }
}