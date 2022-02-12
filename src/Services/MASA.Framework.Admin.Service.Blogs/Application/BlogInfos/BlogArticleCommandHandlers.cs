using Flurl.Http;
using MASA.Framework.Admin.Contracts.Blogs.BlogInfo.ViewModel;
using MASA.Utils.Caching.Core.Interfaces;
using MASA.Utils.Caching.Core.Models;
using Nest;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos
{
    public class BlogArticleCommandHandler
    {
        private const string BaiduTextAIToken = "BaiduTextAIToken";
        private string _defaultIndex;
        private readonly ElasticClient _elasticClient;
        private readonly BlogAppSettiings _appSettiings;
        private ILogger<BlogArticleCommandHandler> _logger;
        private readonly IDistributedCacheClient _redisClient;
        private readonly IBlogArticleRepository _articleRepository;
        private readonly IBlogLabelRepository _blogLabelRepository;
        private readonly IBlogApprovedRecordRepository _approvedRecordRepository;

        public BlogArticleCommandHandler(
            IOptions<BlogAppSettiings> settings,
            IDistributedCacheClient redisClient,
            IBlogArticleRepository articleRepository,
            IBlogLabelRepository blogLabelRepository,
            ILogger<BlogArticleCommandHandler> logger,
            IElasticClientProvider elasticClientProvider,
            IBlogApprovedRecordRepository approvedRecordRepository)
        {
            _logger = logger;
            _redisClient = redisClient;
            _appSettiings = settings.Value;
            _articleRepository = articleRepository;
            _blogLabelRepository = blogLabelRepository;
            _defaultIndex = $"{settings.Value.ElasticConfig.IndexPrefix}_{nameof(BlogInfo)}".ToLower();
            _elasticClient = elasticClientProvider.GetClient(_defaultIndex);
            _approvedRecordRepository = approvedRecordRepository;
        }

        [EventHandler(1)]
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
                ReleaseTime = DateTime.UtcNow,
                CreatorUserId = command.Request.UserId,
                LastModifierUserId = command.Request.UserId,
                WithdrawReason = string.Empty
            };

            var blog = await _articleRepository.CreateAsync(blogInfo);
            command.Request.Id = blog.Id;
            await InsertEsAsync(blog);
            await AddLabelRelations(command.Request.Labels, blog.Id);
        }

        [EventHandler(2)]
        public async Task ContentDefinedAsync(CreateBlogInfoCommand command)
        {
            var (isSuccess, errMsg) = await BaiduDefined(
                command.Request.Title + " " + command.Request.Content);
            if (!isSuccess)
            {
                await WithdrawAsync(new (new WithdrawBlogArticleModel
                {
                    Id = command.Request.Id,
                    ReasonType = ReasonTypes.Other,
                    ReasonDetail = errMsg
                }));
            }
        }

        [EventHandler]
        public async Task UpdateAsync(UpdateBlogInfoCommand command)
        {
            var blogInfo = new BlogInfo()
            {
                Id = command.Request.Id,
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
            article!.WithdrawReason = $"{command.Model.ReasonType.GetDescription().Description}";
            if (command.Model.ReasonDetail != null)
            {
                article!.WithdrawReason += ":" + command.Model.ReasonDetail;
            }

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

        /// <summary>
        /// 获取百度token
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetBaiduAIAccessToken()
        {
            try
            {
                //TODO: redis redisvalue error
                //var token = await _redisClient.GetAsync<string>(BaiduTextAIToken);
                //if(!string.IsNullOrWhiteSpace(token))
                //    return token;

                var authHost = _appSettiings.BaiduAIConfig.GetAccessTokenUrl +
                    "?grant_type=client_credentials"+
                    $"&client_id={_appSettiings.BaiduAIConfig.APIKey}"+
                    $"&client_secret={_appSettiings.BaiduAIConfig.SecretKey}";
                var res = await authHost.PostAsync().ReceiveJson<BaiduAccessTokenViewModel>();
                if (res is not null && !string.IsNullOrWhiteSpace(res.access_token))
                {
                    return res.access_token;
                    //token = res.access_token;
                    //var expiresIn = TimeSpan.FromSeconds(res.expires_in - 60 * 60 * 2);
                    //try
                    //{

                    //    await _redisClient.SetAsync(BaiduTextAIToken, token, new CombinedCacheEntryOptions<string>
                    //    {
                    //        DistributedCacheEntryOptions = new DistributedCacheEntryOptions
                    //        {
                    //            //提前两天过期
                    //            AbsoluteExpirationRelativeToNow = expiresIn
                    //        }
                    //    });
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw;
                    //}
                }

                return string.Empty;
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync<BaiduAccessTokenErrorViewModel>();
                _logger.LogError(ex, $"GetAccessToken异常, error：{error}");

                return string.Empty;
            }
        }

        /// <summary>
        /// 百度文本校验（屏蔽字）
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private async Task<(bool, string)> BaiduDefined(string text)
        {
            try
            {
                var token = await GetBaiduAIAccessToken();
                if (string.IsNullOrWhiteSpace(token))
                    return (false, "自动校验失败，待人工审核！");

                var res = await $"{_appSettiings.BaiduAIConfig.PostUserDefined}?access_token={token}"
                    .WithHeader("Connection", "keep-alive")
                    .WithHeader("Keep-Alive", "600")
                    .PostUrlEncodedAsync(new { text = text })
                    .ReceiveJson<BaiduDefinedViewModel>();

                return (res.IsSuccess, res.data?.First().msg ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "百度文本校验异常", text);
                return (false, "自动校验失败，待人工审核！");
            }
        }
    }
}
