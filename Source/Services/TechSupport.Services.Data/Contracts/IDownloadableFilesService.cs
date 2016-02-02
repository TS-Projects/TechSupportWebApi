namespace TechSupport.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TechSupport.Data.Models;
    using TechSupport.Services.Common;
    using TechSupport.Services.Data.Models;

    public interface IDownloadableFilesService : IService
    {
        Task<File> FileById(int id);

        Task<IEnumerable<File>> AddNew(IEnumerable<RawFile> rawFiles);
    }
}