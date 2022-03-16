namespace Masa.Framework.Admin.Web.Shared
{
    public partial class Favorite
    {
        List<string> _favoriteMenus = new();

        List<string> FavoriteMenus
        {
            get
            {
                if (GlobalConfig.Favorite is null && _favoriteMenus.Count == 0)
                {
                    if (NavHelper.BottomLevelNavs.Count <= 3) _favoriteMenus = NavHelper.BottomLevelNavs.Select(n => n.Code).ToList();
                    else _favoriteMenus = NavHelper.BottomLevelNavs.GetRange(0, 3).Select(n => n.Code).ToList();
                }

                return _favoriteMenus;
            }
            set { _favoriteMenus = value; }
        }

        [Parameter]
        public NavHelper NavHelper { get; set; }

        [Parameter]
        public string CurrentUri { get; set; }

        protected override void OnInitialized()
        {
            if (GlobalConfig.Favorite == "")
            {
                FavoriteMenus.Clear();
            }
            else if (GlobalConfig.Favorite is not null)
            {
                FavoriteMenus = GlobalConfig.Favorite.Split('|').ToList();
            }
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

            if (search is null || search == "") output.AddRange(NavHelper.BottomLevelNavs.Where(n => FavoriteMenus.Contains(n.Code)));
            else
            {
                output.AddRange(NavHelper.BottomLevelNavs.Where(n => GetI18nFullTitle(n.FullTitle).Contains(search, StringComparison.OrdinalIgnoreCase)));
            }

            return output;
        }

        List<NavModel> GetFavoriteMenus() => GetNavs(null);

        void AddOrRemoveFavoriteMenu(string code)
        {
            if (FavoriteMenus.Contains(code)) FavoriteMenus.Remove(code);
            else FavoriteMenus.Add(code);
            GlobalConfig.Favorite = string.Join("|", FavoriteMenus);
        }

        string GetI18nFullTitle(string fullTitle)
        {
            var arr = fullTitle.Split(' ').ToList();
            if (arr.Count == 1) return T(fullTitle);
            else
            {
                return string.Join(" ", arr.Select(a => T(a)));
            }
        }
    }
}
