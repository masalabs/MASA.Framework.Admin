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
            if (command.Model != null)
            {
                var model = new Domain.Entities.Dic
                {
                    CreateTime = DateTimeOffset.UtcNow,
                    Description = command.Model.Description,
                    Enable = command.Model.Enable,
                    Id = Guid.NewGuid(),
                    ModuleId = command.Model.ModuleId,
                    Name = command.Model.Name,
                    Type = command.Model.Type,
                };

                await _dicRepository.AddAsync(model);

                command.Result = model.Id;
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [EventHandler(1, FailureLevels.Ignore, enableRetry: false, isCancel: true)]
        public async Task CancelAddAsync(AddDicCommand command)
        {
            // await _dicRepository.DeleteAsync(command.Dic.Id);
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



        [EventHandler]
        public async Task UpdateAsync(UpdateDicCommand command)
        {
            if (command.Id != default)
            {
                var model = new Domain.Entities.Dic
                {
                    Description = command.Model.Description,
                    Enable = command.Model.Enable,
                    Id = command.Model.Id,
                    ModuleId = command.Model.ModuleId,
                    Name = command.Model.Name,
                    Type = command.Model.Type,
                };

                await _dicRepository.UpdateAsync(model);

                command.Result = model.Id;
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
