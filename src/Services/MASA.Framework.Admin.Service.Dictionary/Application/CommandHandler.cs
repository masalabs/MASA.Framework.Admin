using MASA.Framework.Admin.Service.Dictionary.Application.Dic.Commands;

namespace MASA.Framework.Admin.Service.Dictionary.Application
{
    public class CommandHandler
    {
        private readonly IDicRepository _dicRepository;

        public CommandHandler(IDicRepository dicRepository)
        {
            _dicRepository = dicRepository;
        }

        [EventHandler]
        public async Task AddAsync(AddCommand command)
        {
            if (command.Id != default)
            {
                command.Result = await _dicRepository.AddAsync(command.Dic);
            }
        }

        [EventHandler]
        public async Task Update(UpdateCommand command)
        {
            if (command.Id != default)
            {
                command.Result = await _dicRepository.UpdateAsync(command.Dic);
            }
        }
    }
}
