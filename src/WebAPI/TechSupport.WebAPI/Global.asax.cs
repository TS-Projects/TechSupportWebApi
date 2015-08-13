using Microsoft.OData.Core;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.OData.Formatter;
using System.Web.Optimization;
using System.Web.Routing;
using Elmah.Contrib.WebApi;
using TechSupport.WebAPI.Infrastructure.Mapping;

namespace TechSupport.WebAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RouteTable.Routes.Ignore("{resource}.axd/{*pathInfo}");
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
            FilterConfig.RegisterHttpFilters(GlobalConfiguration.Configuration.Filters);
            GlobalConfiguration.Configuration.Filters.Add(new ElmahHandleErrorApiAttribute());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            (new AutoMapperConfig(Assembly.GetExecutingAssembly())).Execute();
        }
    }
}