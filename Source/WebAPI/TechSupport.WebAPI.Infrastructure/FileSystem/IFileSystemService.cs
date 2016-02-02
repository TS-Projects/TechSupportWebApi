namespace TechSupport.WebAPI.Infrastructure.FileSystem
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using TechSupport.Services.Common;
    using TechSupport.Services.Data.Models;

    using DownloadableFile = TechSupport.Data.Models.File;

    public interface IFileSystemService : IService
    {
        Task SaveDownloadableFiles(IEnumerable<DownloadableFile> files);

        FileStream GetFileStream(string filePath, string fileExtension);
    }
}
