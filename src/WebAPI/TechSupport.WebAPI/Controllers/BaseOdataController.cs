using System.Web.OData;
using TechSupport.Data;
using TechSupport.Data.Models;

namespace TechSupport.WebAPI.Controllers
{
    public abstract class BaseOdataController : ODataController
    {
        public BaseOdataController(ITechSupportData data)
        {
            this.Data = data;
        }

        public BaseOdataController(ITechSupportData data, User profile)
            : this(data)
        {
            this.UserProfile = profile;
        }

        protected ITechSupportData Data { get; set; }

        protected User UserProfile { get; set; }
    }
}