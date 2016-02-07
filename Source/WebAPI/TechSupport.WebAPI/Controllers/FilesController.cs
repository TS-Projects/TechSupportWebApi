//namespace Showcase.Server.Api.Controllers
//{
//    using System.Threading.Tasks;
//    using System.Web.Http;

//    using TechSupport.WebAPI.Controllers.Base;
//    using TechSupport.WebAPI.Infrastructure.Extensions;
//    using TechSupport.WebAPI.Infrastructure.FileSystem;
//    using TechSupport.WebAPI.Infrastructure.Validation;
//    using TechSupport.Services.Data.Contracts;

//    public class FilesController : BaseController
//    {
//        private readonly IDownloadableFilesService downloadableFilesService;
//        private readonly IFileSystemService fileSystemService;

//        public FilesController(IDownloadableFilesService downloadableFilesService, IFileSystemService fileSystemService)
//        {
//            this.downloadableFilesService = downloadableFilesService;
//            this.fileSystemService = fileSystemService;
//        }

//        [HttpGet]
//        [ValidateFilePath]
//        public async Task<IHttpActionResult> Get(int file)
//        {
//            var downloadedFileInfo = await this.downloadableFilesService.FileById(file);
//            var fileStream = this.fileSystemService.GetFileStream(downloadedFileInfo.UrlPath, downloadedFileInfo.FileExtension);
//            return this.File(fileStream, string.Format("{0}.{1}", downloadedFileInfo.OriginalFileName, downloadedFileInfo.FileExtension));
//        }
//    }
//}