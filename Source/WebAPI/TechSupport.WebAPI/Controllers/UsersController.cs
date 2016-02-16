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


    }
}