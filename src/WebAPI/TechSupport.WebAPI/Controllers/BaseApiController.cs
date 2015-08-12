using System.Web.Http;
using TechSupport.Data;
using TechSupport.Data.Models;

namespace TechSupport.WebAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        public BaseApiController(ITechSupportData data)
        {
            this.Data = data;
        }

        public BaseApiController(ITechSupportData data, User profile)
            : this(data)
        {
            this.UserProfile = profile;
        }

        protected ITechSupportData Data { get; set; }

        protected User UserProfile { get; set; }
    }
}