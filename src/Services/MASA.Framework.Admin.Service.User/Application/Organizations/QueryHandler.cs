namespace MASA.Framework.Admin.Service.User.Application.Organizations;

public class QueryHandler
{
    readonly IDepartmentRepository _departmentRepository;

    public QueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    [EventHandler]
    public async Task GetDepartmentTreeAsync(TreeQuery treeQuery)
    {
        treeQuery.Result = await GetDepartmentListAsync(treeQuery.ParentId);
    }

    private async Task<List<DepartmentItemResponse>> GetDepartmentListAsync(Guid parentId)
    {
        var result = new List<DepartmentItemResponse>();
        var departments = await _departmentRepository.GetListAsync(d => d.Id == parentId);
        foreach (var department in departments)
        {
            result.Add(new DepartmentItemResponse
            {
                Id = department.Id,
                Name = department.Name,
                Children = await GetDepartmentListAsync(department.Id)
            });
        }
        return result;
    }
}

