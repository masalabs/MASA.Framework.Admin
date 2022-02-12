namespace MASA.Framework.Admin.Service.Blogs.Application.BlogReport.Commands;

public record IgnoreBlogReportCommand : Command
{
    public IgnoreBlogReportModel Model { get; set; }

    public IgnoreBlogReportCommand()
    {
    }

    public IgnoreBlogReportCommand(IgnoreBlogReportModel model)
    {
        Model = model;
    }
}