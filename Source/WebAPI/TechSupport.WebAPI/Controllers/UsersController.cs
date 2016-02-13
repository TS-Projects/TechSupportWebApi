namespace TechSupport.WebAPI.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using TechSupport.WebAPI.Controllers.Base;
    using TechSupport.WebAPI.DataModels.Users;
    using TechSupport.WebAPI.Infrastructure.Extensions;
    using TechSupport.Services.Data.Contracts;

    public class UsersController : BaseController
    {
        private const int MinimumCharactersForUsernameSearch = 3;

        public UsersController(IUsersService usersService)
            : base(usersService)
        {
        }

        [HttpGet]
        public async Task<IHttpActionResult> Profile(string username)
        {
            var model = await this.UsersService
                .ByUsername(username)
                .ProjectTo<UserNameModel>()
                .FirstOrDefaultAsync();

            return this.Data(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> ProfileData(string username)
        {
            var model = await this.UsersService
                .ByUsername(username)
                .ProjectTo<UserResponseModel>()
                .FirstOrDefaultAsync();

            return this.Data(model);
        }
    }
}