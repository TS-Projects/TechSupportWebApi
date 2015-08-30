using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Elmah.Contrib.WebApi;
using TechSupport.WebAPI.Api.Administration.DataModels;

namespace TechSupport.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<UserProfileDataModel>("Users");
            builder.EntitySet<CustomerCardAdministrationDataModel>("CustomerCard");
            var model = builder.GetEdmModel();
            config.MapODataServiceRoute("odata", "odata", model);
            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());

            config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}