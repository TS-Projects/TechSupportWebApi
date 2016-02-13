namespace TechSupport.WebAPI.Controllers.Base
{
    using System.Web.Http;

    using TechSupport.Services.Data.Contracts;

    public class BaseController : ApiController
    {
        public BaseController(IUsersService usersService)
        {
            this.UsersService = usersService;
        }

        protected IUsersService UsersService { get; private set; }
    }
}