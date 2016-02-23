using System.Net.Http;
using System.Web.Http.Routing;

namespace TechSupport.WebAPI.Config
{
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;

    using Elmah.Contrib.WebApi;

    using TechSupport.WebAPI.Infrastructure.Formatters;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //  config.Formatters.Add(new RazorFormatter());
            config.Formatters.Add(new BrowserJsonFormatter());
            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());

            config.Routes.MapHttpRoute(
               "Files",
               "Files/{folder}/{file}",
               new { controller = "Files", action = "Get" },
               new { folder = @"\d+" });


            config.Routes.MapHttpRoute(
                "CustomServiceRoute",
                "Api/Service/{action}",
                new { controller = "Service", action = "Post" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            //config.Routes.MapHttpRoute(
            //    "CustomerCardsByUser",
            //    "api/CustomerCards/User/{username}",
            //    new {controller = "CustomerCards", action = "CustomerCardsByUser"});

            //config.Routes.MapHttpRoute(
            //   "CustomerCards_Official",
            //   "CustomerCard/Registration/{{action}}/{{id}}",
            //   new { controller = "CustomerCard", action = "Registration", official = true, id = UrlParameter.Optional });

            //config.Routes.MapHttpRoute(
            //     "CustomerCards_Not_Official",
            //     "CustomerCard/Registration/{{action}}/{{id}}",
            //     new { controller = "CustomerCard", action = "Registration", official = true, id = UrlParameter.Optional });

            //config.Routes.MapHttpRoute(
            //     "CustomerCards_by_category",
            //     "CustomerCards/List/ByCategory/{id}/{category}",
            //     new { controller = "List", action = "ByCategory", id = UrlParameter.Optional, category = UrlParameter.Optional });

            //config.Routes.MapHttpRoute(
            //     "UserProfileApi",
            //     "Api/Users/{controller}/{action}/{id}",
            //     new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                "DefaultAdministrationApi",
                "Api/Administration/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                "DefaultApiWithActionAndId",
                "Api/{controller}/{action}/{id}",
                null,
                new { id = @"\d+" });

            config.Routes.MapHttpRoute(
                "UsersApiWithActionAndUsername",
                "Api/Users/{controller}/{username}");

            config.Routes.MapHttpRoute(
                 "ProfileUpdate",
                 "Api/Users/{controller}",
                 new { action = "Post" },
                 new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute(
                "DefaultApiGetWithId",
                "Api/{controller}/{id}",
                new { action = "Get" },
                new { id = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute(
                "DefaultApiPostWithId",
                "Api/{controller}/{id}",
                new { action = "Post" },
                new { id = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute(
                "DefaultApiWithAction",
                "api/{controller}/{action}");

            config.Routes.MapHttpRoute(
                "DefaultApiGet",
                "Api/{controller}",
                new { action = "Get" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute(
                "DefaultApiPost",
                "Api/{controller}",
                new { action = "Post" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            //config.Routes.MapHttpRoute(
            //    "LoginApi",
            //    "api/users/{controller}/{id}",
            //    new { id = RouteParameter.Optional });

            //config.Routes.MapHttpRoute(
            //    "DefaultApi",
            //    "api/{controller}/{id}",
            //    new { id = RouteParameter.Optional });
        }
    }
}