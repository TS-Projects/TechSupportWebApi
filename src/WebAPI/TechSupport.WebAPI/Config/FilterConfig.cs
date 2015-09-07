using System.Web.Http.Filters;
using System.Web.Mvc;
using Elmah.Contrib.WebApi;

namespace TechSupport.WebAPI.Config
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterHttpFilters(HttpFilterCollection filters)
        {
            filters.Add(new ElmahHandleErrorApiAttribute());
        }
    }
}