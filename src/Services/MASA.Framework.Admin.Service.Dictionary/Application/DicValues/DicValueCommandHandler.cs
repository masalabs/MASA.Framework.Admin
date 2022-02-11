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
            if (command.Id != default)
            {
                command.Result = await _dicValueRepository.AddAsync(command.DicValue);
            }
        }

        [EventHandler]
        public async Task UpdateAsync(UpdateDicValueCommand command)
        {
            if (command.Id != default)
            {
                command.Result = await _dicValueRepository.UpdateAsync(command.DicValue);
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
