using CreateCommand = MASA.Framework.Admin.Service.User.Application.Organizations.Commands.CreateCommand;

namespace MASA.Framework.Admin.Service.User.Application.Organizations;

public class CommandHandler
{
    readonly IDepartmentRepository _departmentRepository;

    public CommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    [EventHandler]
    public async Task CreateAsync(CreateCommand createCommand)
    {
        var createDepartmentRequest = createCommand.CreateDepartmentRequest;
        await _departmentRepository.AddAsync(new Department(createDepartmentRequest.Name, createDepartmentRequest.Code, createDepartmentRequest.Describtion, createDepartmentRequest.ParentId));
    }
}

