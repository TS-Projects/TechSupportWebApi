using Microsoft.OData.Core;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.OData.Formatter;
using System.Web.Optimization;
using TechSupport.WebAPI.Infrastructure.Mapping;

namespace TechSupport.WebAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();

            // GlobalConfiguration.Configure(ODataConfig.Register);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            List<ODataPayloadKind> data = new List<ODataPayloadKind>()
            {
                ODataPayloadKind.Value,
                ODataPayloadKind.Property,
                ODataPayloadKind.Collection,
                ODataPayloadKind.Parameter,
                ODataPayloadKind.Entry,
                ODataPayloadKind.Batch
            };
            GlobalConfiguration.Configuration.Formatters.Insert(0, new ODataMediaTypeFormatter(data));
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            (new AutoMapperConfig(Assembly.GetExecutingAssembly())).Execute();
        }
    }
}