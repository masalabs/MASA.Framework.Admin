namespace MASA.Framework.Admin.Contracts.Blogs.BlogInfo.ViewModel
{
    public class BaiduDefinedViewModel
    {
        public string conclusion { get; set; }
        public long log_id { get; set; }
        public Datum[] data { get; set; }
        public bool isHitMd5 { get; set; }
        public int conclusionType { get; set; }
        public bool IsSuccess => "合规".Equals(conclusion);
    }

    public class Datum
    {
        public string msg { get; set; }
        public string conclusion { get; set; }
        public object hits { get; set; }
        public int subType { get; set; }
        public int conclusionType { get; set; }
        public int type { get; set; }
    }
}
