namespace MASA.Framework.Admin.Services.BackgroundJobs.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        public List<Job> List()
        {
            var data = Enumerable.Range(1, 5).Select(index =>
                  new Job
                  {
                      CreateTime = DateTimeOffset.Now,
                      //Id = index,
                      //OrderNumber = DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                      //Address = $"Address {index}"
                  }).ToList();
            return data;
        }
    }
}