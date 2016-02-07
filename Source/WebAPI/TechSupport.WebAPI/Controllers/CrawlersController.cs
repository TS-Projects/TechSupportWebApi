//namespace TechSupport.Server.Api.Controllers
//{
//    using System.Data.Entity;
//    using System.Threading.Tasks;
//    using System.Web.Http;

//    using AutoMapper.QueryableExtensions;

//    using TechSupport.WebAPI;
//    using TechSupport.WebAPI.DataModels;
//    using TechSupport.Services.Data.Contracts;

//    public class CrawlersController : BaseController
//    {
//        private readonly IProjectsService projectsService;

//        public CrawlersController(IProjectsService projectsService)
//        {
//            this.projectsService = projectsService;
//        }

//        [HttpGet]
//        public async Task<IHttpActionResult> Get(int id)
//        {
//            var model = await this.projectsService
//                .ProjectById(id)
//                .Project()
//                .To<ProjectCrawlerResponseModel>()
//                .FirstOrDefaultAsync();

//            model.HostUrl = this.Request.RequestUri.Authority;

//            return this.Ok(model);
//        }
//    }
//}