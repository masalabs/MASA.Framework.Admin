using MASA.Framework.Admin.Service.Dictionary.Application.DicValue.Commands;

namespace MASA.Framework.Admin.Service.Dictionary.Application.DicValues
{
    public class DicValueCommandHandler
    {
        private readonly IDicValueRepository _dicValueRepository;

        public DicValueCommandHandler(IDicValueRepository dicValueRepository)
        {
            _dicValueRepository = dicValueRepository;
        }

        [EventHandler]
        public async Task AddAsync(AddDicValueCommand command)
        {
            if (command.Model != null)
            {
                var model = new Domain.Entities.DicValue
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    Description = command.Model.Description,
                    DicId = command.Model.DicId,
                    Enable = command.Model.Enable,
                    Lable = command.Model.Lable,
                    Sort = command.Model.Sort,
                    Value = command.Model.Value,
                };

                var result = await _dicValueRepository.AddAsync(model);

                command.Result = result.Id;
            }
        }

        [EventHandler]
        public async Task UpdateAsync(UpdateDicValueCommand command)
        {
            if (command.Model.Id != default)
            {
                var model = new Domain.Entities.DicValue
                {
                    Id = Guid.NewGuid(),
                    Description = command.Model.Description,
                    Enable = command.Model.Enable,
                    Lable = command.Model.Lable,
                    Sort = command.Model.Sort,
                    Value = command.Model.Value,
                };

                var reuslt = await _dicValueRepository.UpdateAsync(model);

                command.Result = reuslt.Id;
            }
        }

        [EventHandler]
        public async Task DeleteAsync(DeleteDicValueCommand command)
        {
            if (command.id != default)
            {
                await _dicValueRepository.DeleteAsync(command.id);
            }
        }

        [EventHandler]
        public async Task DeleteAllAsync(DeleteAllDicValueCommand command)
        {
            if (command.ids.Count > 0)
            {
                await _dicValueRepository.DeleteAllAsync(command.ids);
            }
        }
    }
}
