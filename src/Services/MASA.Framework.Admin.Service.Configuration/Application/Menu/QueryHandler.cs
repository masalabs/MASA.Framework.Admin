namespace MASA.Framework.Admin.Configuration.Application.Menu;

public class QueryHandler
{
    private readonly IRepository<Domain.Aggregate.Menu> _repository;

    public QueryHandler(IRepository<Domain.Aggregate.Menu> repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetListAsync(MenuQuery.ListQuery query)
    {
        Expression<Func<Domain.Aggregate.Menu, bool>> condition = menu => true;
        if (!string.IsNullOrEmpty(query.Name))
        {
            condition = condition.And(menu => menu.Name.Contains(query.Name));
        }
        var menus = await _repository.GetPaginatedListAsync(condition, new PaginatedOptions()
        {
            Page = query.PageIndex,
            PageSize = query.PageSize
        });
        query.Result = new PaginatedItemResponse<MenuItemResponse>(
            query.PageIndex,
            query.PageSize,
            menus.Total,
            menus.TotalPages,
            menus.Result.Select(menu => new MenuItemResponse()
            {
                Id = menu.Id,
                Code = menu.Code,
                Name = menu.Name,
                Describe = menu.Describe,
                Icon = menu.Icon,
                Url = menu.Url,
                ParentId = menu.ParentId,
                ParentName = menu.ParentName,
                Sort = menu.Sort,
                Disabled = menu.State == State.Disabled,
                CreationTime = menu.CreationTime
            }));
    }

    [EventHandler]
    public async Task GetAllAsync(MenuQuery.AllQuery query)
    {
        var menus = await _repository.GetListAsync();
        query.Result = menus.Select(menu => new MenuItemResponse()
        {
            Id = menu.Id,
            Code = menu.Code,
            Name = menu.Name,
            Describe = menu.Describe,
            Icon = menu.Icon,
            Url = menu.Url,
            ParentId = menu.ParentId,
            ParentName = menu.ParentName,
            Sort = menu.Sort,
            Disabled = menu.State == State.Disabled,
            CreationTime = menu.CreationTime
        }).ToList();
    }

    [EventHandler]
    public async Task AnyChildAsync(MenuQuery.AnyChildQuery query)
    {
        var anyChild = await _repository.GetCountAsync(m => m.ParentId == query.menuId);
        query.Result = anyChild > 0;
    }
}
