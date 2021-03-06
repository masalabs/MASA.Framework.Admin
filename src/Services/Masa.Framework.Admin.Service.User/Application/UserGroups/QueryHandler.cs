using ListQuery = Masa.Framework.Admin.Service.User.Application.UserGroups.Queries.ListQuery;

namespace Masa.Framework.Admin.Service.User.Application.UserGroups
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
            var userGroups = await _userGroupRepository.GetListAsync();
            selectQuery.Result = userGroups.Select(userGroup => new UserGroupItemResponse()
            {
                Id = userGroup.Id,
                Name = userGroup.Name,
                Code = userGroup.Code,
                Describtion = userGroup.Describtion
            }).ToList();
        }

        [EventHandler]
        public async Task UserGroupListAsync(UserGroupQuery userGroupQuery)
        {
            var userGroups = await _userGroupRepository.GetListAsync(ug => ug.UserGroupItems.Any(ugi => ugi.UserId == userGroupQuery.UserId));
            userGroupQuery.Result = userGroups.Select(userGroup => new UserGroupItemResponse
            {
                Id = userGroup.Id,
                Code = userGroup.Code,
                Name = userGroup.Name,
                Describtion = userGroup.Describtion,
                CreationTime = userGroup.CreationTime,
                ModificationTime = userGroup.ModificationTime
            }).ToList();
        }

        [EventHandler]
        public async Task GroupUserListAsync(GroupUserQuery groupUserQuery)
        {
            var userGroup = await _userGroupRepository.GetByIdAsync(groupUserQuery.GroupId);
            if (userGroup is null)
            {
                throw new UserFriendlyException("usergroupid not found");
            }
            groupUserQuery.Result = userGroup.UserGroupItems.Select(ugi => ugi.User)
                .Select(user => new UserItemResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                }).ToList();
        }

        [EventHandler]
        public async Task GroupPermissionListAsync(PermissionIdsQuery permissionIdsQuery)
        {
            var userGroup = await _userGroupRepository.GetByIdAsync(permissionIdsQuery.GroupId);
            if (userGroup is null)
            {
                throw new UserFriendlyException("usergroupid not found");
            }
            permissionIdsQuery.Result = userGroup.UserGroupPermissions.Select(ugp => ugp.PermissionId).ToList();
        }
    }
}
