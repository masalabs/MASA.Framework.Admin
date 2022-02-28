namespace Masa.Framework.Admin.Service.LogStatistics.Infrastructure.Jobs
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class JobAttribute : Attribute
    {
        public JobAttribute(string name, string group)
        {
            Name = name;
            Group = group;
        }

        public JobAttribute(string name, string group, string cornExpression)
            : this(name, group)
        {
            CornExpression = cornExpression;
        }

        public string Name { get; set; }
        public string Group { get; set; }
        public string CornExpression { get; set; }
    }
}
