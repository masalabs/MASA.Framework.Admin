using MASA.Contrib.Dispatcher.Events;
using MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Commands;
using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys;
using MASA.Framework.Admin.Service.Blogs.Helper;
using MASA.Framework.Admin.Service.Blogs.Model;
using Microsoft.Extensions.Options;
using Nest;
using Newtonsoft.Json;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos
{
    public class BlogArticleCommandHandler
    {
        private string _defaultIndex;
        private readonly ElasticClient _elasticClient;
        private ILogger<BlogArticleCommandHandler> _logger;
        private readonly IBlogArticleRepository _articleRepository;
        private readonly IBlogLabelRepository _blogLabelRepository;

        public BlogArticleCommandHandler(
            IOptions<BlogAppSettiings> settings,
            IBlogArticleRepository articleRepository,
            IBlogLabelRepository blogLabelRepository,
            ILogger<BlogArticleCommandHandler> logger,
            IElasticClientProvider elasticClientProvider)
        {
            _logger = logger;
            _articleRepository = articleRepository;
            _blogLabelRepository = blogLabelRepository;
            _defaultIndex = $"{settings.Value.ElasticConfig.IndexPrefix}_{nameof(BlogInfo)}".ToLower();
            _elasticClient = elasticClientProvider.GetClient(_defaultIndex);
        }

        [EventHandler]
        public async Task CreateAsync(CreateBlogInfoCommand command)
        {
            var blogInfo = new BlogInfo
            {
                Title = command.Request.Title,
                State = command.Request.State,
                TypeId = command.Request.TypeId,
                Content = command.Request.Content,
                IsShow = command.Request.IsShow,
                Visits = 0,
                CommentCount = 0,
                ApprovedCount = 0,
                ReleaseTime = DateTime.UtcNow
            };
            await _articleRepository.CreateAsync(blogInfo);
            await InsertEsAsync(blogInfo);

            if (command.Request.Labels is not null && command.Request.Labels.Count > 0)
                await _blogLabelRepository.CreateBatchAsync(command.Request.Labels);

            //关联关系
        }

        [EventHandler]
        public async Task UpdateAsync(UpdateBlogInfoCommand command)
        {
            await _articleRepository.UpdateAsync(new()
            {
                Id = command.Request.Id,
                Title = command.Request.Title,
                TypeId = command.Request.TypeId,
                Content = command.Request.Content,
                IsShow = command.Request.IsShow,
                State = command.Request.State
            });
        }

        [EventHandler]
        public async Task RemoveAsync(RemoveBlogInfoCommand command)
        {
            await _articleRepository.RemoveAsync(command.Ids);
        }

        /// <summary>
        /// 写入
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
                _logger.LogError(ex, $"ES写入异常：Index：{_defaultIndex}，Data：{JsonConvert.SerializeObject(blogInfo)}");
            }

            return false;
        }
    }
}
