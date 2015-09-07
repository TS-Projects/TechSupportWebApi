﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.OData.Edm;
using TechSupport.WebAPI.Api.Administration.DataModels;

namespace TechSupport.WebAPI.Config
{
    public static class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "odata",
                model: GetEdmModel());
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<UserProfileDataModel>("Users");
            builder.EntitySet<CustomerCardAdministrationDataModel>("CustomerCard");
            return builder.GetEdmModel();
        }
    }
}