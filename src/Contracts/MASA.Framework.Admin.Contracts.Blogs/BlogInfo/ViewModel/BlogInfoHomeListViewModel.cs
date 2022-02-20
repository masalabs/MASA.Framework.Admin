namespace MASA.Framework.Admin.Contracts.Blogs
{
    public record BlogInfoHomeListViewModel(
        Guid Id,
        string Title,
        Guid TypeId,
        string Content,
        DateTime CreationTime,
        DateTime ReleaseTime,
        int ApprovedCount,
        int CommentCount,
        int Visits,
        Guid CreatorUserId
    );
}