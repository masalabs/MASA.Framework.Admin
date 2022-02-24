namespace MASA.Framework.Admin.Configuration.Application.Menu;

public class QueryHandler
{
    private readonly IRepository<Domain.Aggregate.Menu> _repository;
    private readonly DbContext _dbContext;

    public QueryHandler(IRepository<Domain.Aggregate.Menu> repository, ConfigurationDbContext dbContext)
    {
        _repository = repository;
        _dbContext = dbContext;
    }

    [EventHandler]
    public async Task GetListAsync(MenuQuery.ListMenuQuery query)
    {
        Expression<Func<Domain.Aggregate.Menu, bool>> condition = menu => true;
        if (!string.IsNullOrEmpty(query.Name))
        {
            condition = condition.And(menu => menu.Name.Contains(query.Name));
        }

        var list = await (from menu in _dbContext.Set<Domain.Aggregate.Menu>().Where(condition)
            join parentMenu in _dbContext.Set<Domain.Aggregate.Menu>()
                on menu.ParentId equals parentMenu.Id
                into temp
            from newMenu in temp.DefaultIfEmpty()
            select new MenuItemResponse()
            {
                Id = menu.Id,
                Code = menu.Code,
                Name = menu.Name,
                Describe = menu.Describe,
                Icon = menu.Icon,
                Url = menu.Url,
                ParentId = menu.ParentId,
                ParentName = newMenu.Name,
                Sort = menu.Sort,
                Disabled = !menu.Enable,
                CreationTime = menu.CreationTime
            }).Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToListAsync();

        var count = await _repository.GetCountAsync(condition);
        var menus = await _repository.GetPaginatedListAsync(condition, new PaginatedOptions()
        {
            Page = query.PageIndex,
            PageSize = query.PageSize
        });
        query.Result = new PaginatedItemResponse<MenuItemResponse>(
            query.PageIndex,
            query.PageSize,
            count,
            long.Parse(Math.Ceiling(count / (query.PageSize * 1.0d)).ToString(CultureInfo.InvariantCulture)),
            list);
    }

    [EventHandler]
    public async Task GetAllAsync(MenuQuery.AllMenuQuery query)
    {
        query.Result = await (from menu in _dbContext.Set<Domain.Aggregate.Menu>()
            join parentMenu in _dbContext.Set<Domain.Aggregate.Menu>()
                on menu.ParentId equals parentMenu.Id
                into temp
            from newMenu in temp.DefaultIfEmpty()
            select new MenuItemResponse()
            {
                Id = menu.Id,
                Code = menu.Code,
                Name = menu.Name,
                Describe = menu.Describe,
                Icon = menu.Icon,
                Url = menu.Url,
                ParentId = menu.ParentId,
                ParentName = newMenu.Name,
                Sort = menu.Sort,
                Disabled = !menu.Enable,
                CreationTime = menu.CreationTime
            }).ToListAsync();
    }

    [EventHandler]
    public async Task AnyChildAsync(MenuQuery.AnyMenuChildQuery query)
    {
        var anyChild = await _repository.GetCountAsync(m => m.ParentId == query.menuId);
        query.Result = anyChild > 0;
    }

    [EventHandler]
    public async Task GetMenuDetailAsync(MenuDetailQuery query)
    {
        var menuInfo = await _repository.FindAsync(query.MenuId);
        if (menuInfo == null)
            throw new UserFriendlyException("the menu is not found");

        var parentName = string.Empty;
        if (menuInfo.ParentId != null)
            parentName = menuInfo.Name;

        query.Result = new MenuInfoResponse()
        {
            Id = menuInfo.Id,
            Code = menuInfo.Code,
            Name = menuInfo.Name,
            Describe = menuInfo.Describe,
            Icon = menuInfo.Icon,
            Url = menuInfo.Url,
            ParentId = menuInfo.ParentId,
            ParentName = parentName,
            Sort = menuInfo.Sort
        };
    }
}