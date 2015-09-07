using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Elmah.Contrib.WebApi;
using TechSupport.WebAPI.Api.Administration.DataModels;

namespace TechSupport.WebAPI.Config
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes

            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());

            //config.Routes.MapHttpRoute(
            //   "Files",
            //   "Files/{folder}/{file}",
            //   new { controller = "Files", action = "Get" },
            //   new { folder = @"\d+" });

            //config.Routes.MapHttpRoute(
            //    "ProjectDetails",
            //    "api/Projects/{id}/{titleUrl}",
            //    new { controller = "Projects", action = "Get" },
            //    new { id = @"\d+" });

            //config.Routes.MapHttpRoute(
            //    "PagedComments",
            //    "api/Comments/{id}/{page}",
            //    new { controller = "Comments", action = "Get" },
            //    new { id = @"\d+", page = @"\d+" });

            //config.Routes.MapHttpRoute(
            //    "PagedCommentsByUser",
            //    "api/Comments/User/{username}/{page}",
            //    new { controller = "Comments", action = "CommentsByUser" },
            //    new { page = @"\d+" });

            //config.Routes.MapHttpRoute(
            //    "DefaultApiWithActionAndId",
            //    "Api/{controller}/{action}/{id}",
            //    null,
            //    new { id = @"\d+" });

            //config.Routes.MapHttpRoute(
            //    "DefaultApiWithActionAndUsername",
            //    "Api/{controller}/{action}/{username}");

            //config.Routes.MapHttpRoute(
            //    "DefaultApiGetWithId",
            //    "Api/{controller}/{id}",
            //    new { action = "Get" },
            //    new { id = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            //config.Routes.MapHttpRoute(
            //    "DefaultApiPostWithId",
            //    "Api/{controller}/{id}",
            //    new { action = "Post" },
            //    new { id = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            //config.Routes.MapHttpRoute(
            //    "DefaultApiWithAction",
            //    "Api/{controller}/{action}");

            //config.Routes.MapHttpRoute(
            //    "DefaultApiGet",
            //    "Api/{controller}",
            //    new { action = "Get" },
            //    new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            //config.Routes.MapHttpRoute(
            //    "DefaultApiPost",
            //    "Api/{controller}",
            //    new { action = "Post" },
            //    new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}