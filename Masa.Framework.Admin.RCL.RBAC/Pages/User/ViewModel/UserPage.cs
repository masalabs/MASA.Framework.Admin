using Masa.Framework.Admin.RCL.RBAC.Data.User.Dto;

namespace Masa.Framework.Admin.RCL.RBAC.Pages.User.ViewModel;

internal class UserPage
{
    public List<UserDto> UserDatas { get; set; }

    public string? Account { get; set; }

    public string? State { get; set; }

    public string? Search { get; set; }

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public int PageCount => (int)Math.Ceiling(CurrentCount / (double)PageSize);

    public int CurrentCount => GetFilterDatas().Count();

    public UserPage(List<UserDto> datas)
    {
        UserDatas = new List<UserDto>();
        UserDatas.AddRange(datas);
    }

    private IEnumerable<UserDto> GetFilterDatas()
    {
        IEnumerable<UserDto> datas = UserDatas;

        if (Search is not null)
        {
            datas = datas.Where(d => d.FullName.Contains(Search, StringComparison.OrdinalIgnoreCase) || d.Email?.Contains(Search, StringComparison.OrdinalIgnoreCase) == true);
        }

        if (datas.Count() < (PageIndex - 1) * PageSize) PageIndex = 1;

        return datas;
    }

    public List<UserDto> GetPageDatas()
    {
        return GetFilterDatas().Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
    }
}

