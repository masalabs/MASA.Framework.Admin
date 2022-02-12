using MASA.Framework.Admin.Infrastructures.FileStoring;
using MASA.Framework.Admin.Service.FileStoring.Helper;
using Microsoft.AspNetCore.Mvc;

namespace MASA.Framework.Admin.Service.FileStoring.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MinioFileStoringController : ControllerBase
    {
        private readonly IFileContainer<MinioFileStoringController> Container;

        public MinioFileStoringController(IFileContainer<MinioFileStoringController> container)
        {
            Container = container;
        }

        [HttpPost]
        public async Task<IResult> SaveAsync(IFormFile file)
        {
            string objectName = $"/{DateTime.Now:yyyy/MM/dd}/{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";//文件保存路径

            if (file.Length > 0)
            {
                Stream stream = file.OpenReadStream();

                await Container.SaveAsync(objectName, stream);
            }
            else
            {
                throw new ArgumentNullException("file cannot be empty");
            }

            return Results.Ok(new { FileName = objectName, Size = file.Length });
        }

        [HttpGet]
        public async Task<FileResult> GetAsync(string fileName)
        {
            var file = await Container.GetOrNullAsync(fileName);

            if (file == null)
                throw new ArgumentNullException("file cannot be empty");

            return File(file, MimeHelper.GetMimeType(fileName));
        }

        [HttpGet]
        public async Task<IResult> ExistsAsync(string fileName)
        {
            var isExist = await Container.ExistsAsync(fileName);

            return Results.Ok(new { IsExist = isExist });
        }

        [HttpDelete]
        public async Task<IResult> DeleteAsync(string fileName)
        {
            var isDelete = await Container.DeleteAsync(fileName);

            return Results.Ok(new { IsDelete = isDelete });
        }
    }
}
