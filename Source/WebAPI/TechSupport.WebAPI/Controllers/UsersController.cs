﻿namespace TechSupport.WebAPI.Controllers
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

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Identity()
        {
            //bool isAdmin = false == !User.IsInRole("Administrator");
            bool isAdmin = false;

            if (User.IsInRole("Administrator"))
            {
                isAdmin = true;
            }

            var model = await this.UsersService
                .ByUsername(this.CurrentUser.UserName)
                .ProjectTo<IdentityResponseModel>(new { isAdmin })
                .FirstOrDefaultAsync();

            return this.Data(model);
        }
    }
}