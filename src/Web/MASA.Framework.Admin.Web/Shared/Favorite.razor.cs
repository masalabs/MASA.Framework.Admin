namespace MASA.Framework.Admin.Web.Shared
{
    public partial class Favorite
    {
        List<string> _favoriteMenus = new();//FavoriteService.GetDefaultFavoriteMenuList();

        protected override void OnInitialized()
        {
            if (GlobalConfig.Favorite == "")
            {
                _favoriteMenus.Clear();
            }
            else if (GlobalConfig.Favorite is not null)
            {
                _favoriteMenus = GlobalConfig.Favorite.Split('|').ToList();
            }

            GlobalConfig.OnCurrentNavChanged += base.StateHasChanged;
        }

        bool _open;
        string? _search;

        void OnOpen(bool open)
        {
            _open = open;
            if (open is true)
            {
                _search = null;
            }
        }

        List<NavModel> GetNavs(string? search)
        {
            var output = new List<NavModel>();

            if (search is null || search == "") output.AddRange(NavHelper.SameLevelNavs.Where(n => _favoriteMenus.Contains(n.Id.ToString())));
            else
            {
                output.AddRange(NavHelper.SameLevelNavs.Where(n => n.Href is not null && GetI18nFullTitle(n.FullTitle).Contains(search, StringComparison.OrdinalIgnoreCase)));
            }

            return output;
        }

        List<NavModel> GetFavoriteMenus() => GetNavs(null);

        void AddOrRemoveFavoriteMenu(string id)
        {
            if (_favoriteMenus.Contains(id)) _favoriteMenus.Remove(id);
            else _favoriteMenus.Add(id);
            GlobalConfig.Favorite = string.Join("|", _favoriteMenus);
        }

        string GetI18nFullTitle(string fullTitle)
        {
            var arr = fullTitle.Split(' ').ToList();
            if (arr.Count == 1) return T(fullTitle);
            else
            {
                var parent = arr[0];
                arr.RemoveAt(0);
                return $"{T(parent)} {T(string.Join(' ', arr))}";
            }
        }

        public void Dispose()
        {
            GlobalConfig.OnCurrentNavChanged -= base.StateHasChanged;
        }
    }
}
