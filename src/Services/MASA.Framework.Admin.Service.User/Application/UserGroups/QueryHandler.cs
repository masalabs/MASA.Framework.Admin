using ListQuery = MASA.Framework.Admin.Service.User.Application.UserGroups.Queres.ListQuery;

namespace MASA.Framework.Admin.Service.User.Application.UserGroups
{
    public class QueryHandler
    {
        readonly IUserGroupRepository _userGroupRepository;

        public QueryHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        [EventHandler]
        public async Task GetListAsync(ListQuery listQuery)
        {
            var userGroups = await _userGroupRepository.GetPaginatedListAsync((u) => string.IsNullOrEmpty(listQuery.Name) || u.Name.Contains(listQuery.Name)
                        , new PaginatedOptions
                        {
                            Page = listQuery.PageIndex,
                            PageSize = listQuery.PageSize,
                        });

            listQuery.Result = new PaginatedItemResponse<UserGroupItemResponse>(
                listQuery.PageIndex,
                listQuery.PageSize,
                userGroups.Total,
                userGroups.TotalPages,
                userGroups.Result.Select(userGroup => new UserGroupItemResponse()
                {
                    Id = userGroup.Id,
                    Name = userGroup.Name,
                    Code = userGroup.Code,
                    Describtion = userGroup.Describtion,
                    CreationTime = userGroup.CreationTime,
                    ModificationTime = userGroup.ModificationTime
                }));
        }

        [EventHandler]
        public async Task SelectListAsync(SelectQuery selectQuery)
        {

        }

        [EventHandler]
        public async Task UserGroupListAsync(UserGroupQuery userGroupQuery)
        {

        }
    }
}
