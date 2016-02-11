using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WebAPI.Common
{
    public class Constants
    {
        public const string DataModelsAssembly = "TechSupport.WebAPI.DataModels";
        public const string InfrastructureAssembly = "TechSupport.WebAPI.Infrastructure";
        public const string DataServicesAssembly = "TechSupport.Services.Data";
        public const string LogicServicesAssembly = "TechSupport.Services.Logic";

        public const string ShortDateFormat = "dd MMM yyyy";
        public const int MaxProjectsPageSize = 128;

        public const int MaxUploadedFileSize = 10485760;

        public const string RequestCannotBeEmpty = "Request cannot be empty";
        public const string RequestedResourceWasNotFound = "The requested resource was not found";
        public const string NotAuthorized = "You are not authorized for this operation";
        public const string InvalidPageNumber = "Invalid page number";

        public const string AutomaticAddedExistSpecificRoleOnRegisterUser = "User";
    }
}
