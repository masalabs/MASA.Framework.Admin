using CreateCommand = Masa.Framework.Admin.Service.User.Application.Organizations.Commands.CreateCommand;

namespace Masa.Framework.Admin.Service.User.Application.Organizations;

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

    [EventHandler]
    public async Task UpdateDepartmentUserAsync(UpdateDepartmentUserCommand updateDepartmentUserCommand)
    {
        var department = await _departmentRepository.GetByIdAsync(updateDepartmentUserCommand.UpdateDepartmentUserRequest.DepartmentId);
        if (department is null)
        {
            throw new UserFriendlyException($"deparment id {updateDepartmentUserCommand.UpdateDepartmentUserRequest.DepartmentId} not found");
        }
        department.UpdateUsers(updateDepartmentUserCommand.UpdateDepartmentUserRequest.UserIds);
        await _departmentRepository.UpdateAsync(department);
    }
}

