namespace TechSupport.WebAPI.Api.Users.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;


    using TechSupport.Services.Data.Contracts;
    using TechSupport.WebAPI.DataModels.Users;

    public class LoginController : ApiController
    {
        private readonly IUsersService usersService;

        public LoginController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var model = this.usersService
                .QueriedAllUsers()
                .Where(u => u.UserName == this.User.Identity.Name)
                .AsQueryable()
                .ProjectTo<IdentityResponseModel>()
                .FirstOrDefault();

            return this.Ok(model);
        }
    }
}