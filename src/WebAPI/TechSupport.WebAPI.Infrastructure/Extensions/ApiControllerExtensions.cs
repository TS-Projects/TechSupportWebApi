using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using TechSupport.WebAPI.Common;
using TechSupport.WebAPI.Infrastructure.Formatters;
using TechSupport.WebAPI.Infrastructure.HttpActionResults;

namespace TechSupport.WebAPI.Infrastructure.Extensions
{
    public static class ApiControllerExtensions
    {
        public static FormattedContentResult<ResultObject> Data(this ApiController apiController, object data)
        {
            if (data == null)
            {
                return apiController.Data(false, Constants.RequestedResourceWasNotFound);
            }

            return new FormattedContentResult<ResultObject>(HttpStatusCode.OK, new ResultObject(data),new BrowserJsonFormatter(), null, apiController );
        }

        public static FormattedContentResult<ResultObject> Data(this ApiController apiController, bool success, string errorMessage, object data = null)
        {
            return new FormattedContentResult<ResultObject>(HttpStatusCode.OK, new ResultObject(success, errorMessage, data), new BrowserJsonFormatter(), null, apiController);
        }

        //public static FileResult File(this ApiController apiController, FileStream fileStream, string fileName)
        //{
        //    return new FileResult(fileStream, fileName);
        //}
    }
}
