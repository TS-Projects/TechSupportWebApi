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

    public class UsersController : BaseAuthorizationController
    {
        private const int MinimumCharactersForUsernameSearch = 3;

        public UsersController(IUsersService usersService)
            : base(usersService)
        {
        }

        [HttpGet]
        public async Task<IHttpActionResult> Profile(string username)
        {
            string visitorUsername = null;
            string visitorEmail = null;
            var isAdmin = false;
            if (this.CurrentUser != null)
            {
                visitorUsername = this.CurrentUser.UserName;
                visitorEmail = this.CurrentUser.Email;
                isAdmin = this.CurrentUser.IsAdmin;
            }

            var model = await this.UsersService
                .ByUsername(username)
                .ProjectTo<UserResponseModel>(new { username, visitorEmail, visitorUsername, isAdmin })
                .FirstOrDefaultAsync();

            return this.Data(model);
        }



        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Identity()
        {
            var model = await this.UsersService
                .ByUsername(this.CurrentUser.UserName)
                .ProjectTo<UserResponseModel>()
                .FirstOrDefaultAsync();

            return this.Data(model);
        }
    }
}