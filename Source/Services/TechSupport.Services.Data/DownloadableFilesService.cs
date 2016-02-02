namespace TechSupport.Services.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using TechSupport.Data.Common.Repositories;
    using TechSupport.Data.Models;
    using TechSupport.Services.Common.Extensions;
    using TechSupport.Services.Data.Base;
    using TechSupport.Services.Data.Contracts;
    using TechSupport.Services.Data.Models;
    using TechSupport.Services.Logic.Contracts;

    public class DownloadableFilesService : FileInfoService, IDownloadableFilesService
    {
        private readonly IRepository<File> files;

        public DownloadableFilesService(IObjectFactory objectFactory, IRepository<File> files)
            : base(objectFactory)
        {
            this.files = files;
        }

        public async Task<File> FileById(int id)
        {
            return await this.files
                .All()
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<File>> AddNew(IEnumerable<RawFile> rawFiles)
        {
            var addedFiles = await rawFiles.ForEachAsync(async rawFile =>
            {
                var file = await this.SaveFileInfo<File>(rawFile);
                file.Content = rawFile.Content;
                return file;
            });

            return addedFiles;
        }
    }
}