using MASA.Contrib.Dispatcher.Events.Enums;
using MASA.Framework.Admin.Service.Dictionary.Application.Dic.Commands;
using MASA.Utils.Caching.Core.Interfaces;
using MASA.Utils.Caching.DistributedMemory.Interfaces;

namespace MASA.Framework.Admin.Service.Dictionary.Application.Dic
{
    public class DicCommandHandler
    {
        private readonly IDicRepository _dicRepository;
        private readonly IDistributedCacheClient _distributedCacheClient;

        public DicCommandHandler(IDicRepository dicRepository, IDistributedCacheClient distributedCacheClient)
        {
            _dicRepository = dicRepository;
            _distributedCacheClient = distributedCacheClient;
        }

        [EventHandler(1)]
        public async Task AddAsync(AddDicCommand command)
        {
            if (command.Id != default)
            {
                command.Result = await _dicRepository.AddAsync(command.Dic);
            }
        }

        /// <summary>
        /// TODO 缓存报错,处理后再解决
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [EventHandler(2, FailureLevels.ThrowAndCancel, enableRetry: true, retryTimes: 3)]
        public async Task AddCacheAsync(AddDicCommand command)
        {
            //try
            //{
            //    if (command.Id != default)
            //    {
            //        await _distributedCacheClient.SetAsync($"Dic-{command.Id}", command.Dic, new()
            //        {
            //            DistributedCacheEntryOptions = new()
            //            {
            //                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3)
            //            },
            //            MemoryCacheEntryOptions = new()
            //            {
            //                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3)
            //            },
            //        });

            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [EventHandler(2, FailureLevels.Ignore, enableRetry: false, isCancel: true)]
        public async Task CancelAddAsync(AddDicCommand command)
        {
            // await _dicRepository.DeleteAsync(command.Dic.Id);
        }

        [EventHandler]
        public async Task UpdateAsync(UpdateDicCommand command)
        {
            if (command.Id != default)
            {
                command.Result = await _dicRepository.UpdateAsync(command.Dic);
            }
        }

        [EventHandler]
        public async Task DeleteAsync(DeleteDicCommand command)
        {
            if (command.id != default)
            {
                await _dicRepository.DeleteAsync(command.id);
            }
        }

        [EventHandler]
        public async Task DeleteAllAsync(DeleteAllDicCommand command)
        {
            if (command.ids.Count > 0)
            {
                await _dicRepository.DeleteAllAsync(command.ids);
            }
        }
    }
}
