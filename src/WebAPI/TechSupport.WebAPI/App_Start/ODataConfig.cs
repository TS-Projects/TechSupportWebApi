//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Http;
//using TechSupport.Data.Models;
//using TechSupport.WebAPI.DataModels;
//using System.Web.Http.OData.Extensions;
//using System.Web.Http.OData.Builder;

//namespace TechSupport.WebAPI.App_Start
//{
//    public static class ODataConfig
//    {
//        public static void Register(HttpConfiguration config)
//        {
//            ODataModelBuilder builder = new ODataConventionModelBuilder();
//            builder.EntitySet<UserAdministrationDataModel>("Users");
//            builder.EntitySet<Employee>("Employees");
//            config.EnableCors();
//            var model = builder.GetEdmModel();
//            config.Routes.MapODataServiceRoute("odata", "odata", model);

//          //  config.EnableQuerySupport();
//        }
//    }
//}