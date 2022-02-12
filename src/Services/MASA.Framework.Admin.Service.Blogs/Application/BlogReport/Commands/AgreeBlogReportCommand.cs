namespace MASA.Framework.Admin.Service.Blogs.Application.BlogReport.Commands;

public record AgreeBlogReportCommand : Command
{
    public AgreeBlogReportModel Model { get; set; }

    public AgreeBlogReportCommand()
    {
    }

    public AgreeBlogReportCommand(AgreeBlogReportModel model)
    {
        Model = model;
    }
}