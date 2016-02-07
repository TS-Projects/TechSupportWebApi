namespace TechSupport.WebAPI.Controllers.Base
{
    using System.Web.OData;

    using TechSupport.Data.Models;
    using TechSupport.Services.Data.Contracts;

    public class BaseODataAuthorizationController : ODataController
    {
        public BaseODataAuthorizationController(IUsersService usersService)
        {
            this.UsersService = usersService;

            this.CurrentUser = new User
            {
                UserName = "SomeUser",
                IsAdmin = true,
            };
        }

        protected IUsersService UsersService { get; private set; }

        protected User CurrentUser { get; set; }
    }
}