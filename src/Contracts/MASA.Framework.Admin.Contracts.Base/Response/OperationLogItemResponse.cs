namespace MASA.Framework.Admin.Contracts.Base.Response
{
    public class OperationLogItemResponse
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string CreateTime { get; set; }

        public string UserName { get; set; }
    }
}
