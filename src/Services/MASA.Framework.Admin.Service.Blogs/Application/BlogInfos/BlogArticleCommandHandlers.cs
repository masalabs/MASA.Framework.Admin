using Nest;
using System.Text.Json;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos
{
    public class BlogArticleCommandHandler
    {
        private string _defaultIndex;
        private readonly ElasticClient _elasticClient;
        private ILogger<BlogArticleCommandHandler> _logger;
        private readonly IBlogArticleRepository _articleRepository;
        private readonly IBlogLabelRepository _blogLabelRepository;
        private readonly IBlogApprovedRecordRepository _approvedRecordRepository;

        public BlogArticleCommandHandler(
            IOptions<BlogAppSettiings> settings,
            IBlogArticleRepository articleRepository,
            IBlogLabelRepository blogLabelRepository,
            ILogger<BlogArticleCommandHandler> logger,
            IElasticClientProvider elasticClientProvider,
            IBlogApprovedRecordRepository approvedRecordRepository)
        {
            _logger = logger;
            _articleRepository = articleRepository;
            _blogLabelRepository = blogLabelRepository;
            _defaultIndex = $"{settings.Value.ElasticConfig.IndexPrefix}_{nameof(BlogInfo)}".ToLower();
            _elasticClient = elasticClientProvider.GetClient(_defaultIndex);
            _approvedRecordRepository = approvedRecordRepository;
        }

        [EventHandler]
        public async Task CreateAsync(CreateBlogInfoCommand command)
        {
            var blogInfo = new BlogInfo
            {
                Id = Guid.NewGuid(),
                Title = command.Request.Title,
                State = command.Request.State,
                TypeId = command.Request.TypeId,
                Content = command.Request.Content,
                IsShow = command.Request.IsShow,
                Visits = 0,
                CommentCount = 0,
                ApprovedCount = 0,
                ReleaseTime = DateTime.UtcNow,
                CreatorUserId = command.Request.UserId,
                LastModifierUserId = command.Request.UserId
            };

            var blog = await _articleRepository.CreateAsync(blogInfo);
            await InsertEsAsync(blog);
            await AddLabelRelations(command.Request.Labels, blog.Id);
        }

        [EventHandler]
        public async Task UpdateAsync(UpdateBlogInfoCommand command)
        {
            var blogInfo = new BlogInfo()
            {
                Title = command.Request.Title,
                TypeId = command.Request.TypeId,
                Content = command.Request.Content,
                IsShow = command.Request.IsShow,
                State = command.Request.State
            };
            await _articleRepository.UpdateAsync(blogInfo);
            await InsertEsAsync(blogInfo);

            await AddLabelRelations(command.Request.AddLabels, blogInfo.Id);

            if (command.Request.DeleteRelationIds is not null && 
                command.Request.DeleteRelationIds.Count > 0)
                await _blogLabelRepository.DeleteBlogLabelRelationBatchAsync(command.Request.DeleteRelationIds);
        }

        [EventHandler]
        public async Task RemoveAsync(RemoveBlogInfoCommand command)
        {
            await _articleRepository.RemoveAsync(command.Ids);
            foreach (var blogId in command.Ids)
            {
                await RemoveEsAsync(blogId);
            }
        }

        /// <summary>
        /// 追加阅读数
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [EventHandler]
        public async Task AddVisits(AddBlogVisitCommand command)
        {
            await _articleRepository.AddVisits(command.Request);
        }

        /// <summary>
        /// 点赞、取消点赞记录
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [EventHandler]
        public async Task AddBlogApprovedRecord(AddBlogApprovedRecordCommand command)
        {
            var blog = await _approvedRecordRepository.AddBlogApprovedRecord(command.Request);
            await InsertEsAsync(blog);
        }

        /// <summary>
        /// 下架文章
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [EventHandler]
        public async Task WithdrawAsync(WithdrawBlogArticleCommand command)
        {
            var article = await _articleRepository.GetAsync(command.Model.Id);
            article!.State = StateTypes.OffTheShelf;

            await _articleRepository.UpdateAsync(article);
        }

        /// <summary>
        /// 添加博文标签
        /// </summary>
        /// <param name="labels"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        private async Task AddLabelRelations(List<string> labels, Guid blogId)
        {
            if (labels is not null && labels.Count > 0)
            {
                var labelIds = await _blogLabelRepository.CreateBatchAsync(labels);

                if (labelIds is not null && labelIds.Count > 0)
                {
                    await _blogLabelRepository.CreateBlogLabelRelationBatchAsync(
                        labelIds.Select(x => new BlogLabelRelationship
                        {
                            BlogInfoId = blogId,
                            BlogLabelId = x
                        }));
                }
            }
        }

        /// <summary>
        /// 写入修改
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task<bool> InsertEsAsync(BlogInfo blogInfo)
        {
            try
            {
                var res = await _elasticClient.IndexDocumentAsync(blogInfo);
                if (res.IsValid && (res.Result == Result.Created || res.Result == Result.Updated))
                    return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ES写入异常：Index：{_defaultIndex}，Data：{JsonSerializer.Serialize(blogInfo)}");
            }

            return false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task<bool> RemoveEsAsync(Guid blogId)
        {
            try
            {
                var res = await _elasticClient.DeleteAsync<BlogInfo>(blogId);
                if (res.IsValid && res.Result == Result.Deleted)
                    return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ES删除异常：Index：{_defaultIndex}，blogId：{blogId}");
            }

            return false;
        }
    }
}
