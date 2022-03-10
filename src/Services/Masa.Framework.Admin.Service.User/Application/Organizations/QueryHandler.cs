namespace Masa.Framework.Admin.Service.User.Application.Organizations;

public class QueryHandler
{
    readonly IDepartmentRepository _departmentRepository;
    readonly IUserRepository _userRepository;

    public QueryHandler(IDepartmentRepository departmentRepository, IUserRepository userRepository)
    {
        _departmentRepository = departmentRepository;
        _userRepository = userRepository;
    }

    [EventHandler]
    public async Task GetDepartmentUserAsync(DepartmentUserQuery departmentUserQuery)
    {
        var users = await _userRepository.QueryListAsync((e) => true, nameof(Domain.Aggregates.User.DepartmentUsers));
        departmentUserQuery.Result = users.Select(user => new UserItemResponse()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            State = Convert.ToInt32(user.Enable),
            Cover = user.Cover,
            Gender = user.Gender,
            Select = user.DepartmentUsers.Any(d => d.Department.Id == departmentUserQuery.DepartmentId),
        }).ToList();
    }

    [EventHandler]
    public async Task GetDepartmentTreeAsync(TreeQuery treeQuery)
    {
        treeQuery.Result = await GetDepartmentListAsync(treeQuery.ParentId);
    }

    private async Task<List<DepartmentItemResponse>> GetDepartmentListAsync(Guid parentId)
    {
        var result = new List<DepartmentItemResponse>();
        //todo change memory
        var departments = await _departmentRepository.GetListAsync(d => d.ParentId == parentId);
        foreach (var department in departments)
        {
            var item = new DepartmentItemResponse
            {
                Id = department.Id,
                Name = department.Name,
                Children = await GetDepartmentListAsync(department.Id)
            };
            result.Add(item);
        }
        return result;
    }
}

