using TechSupport.Data.Models;
using TechSupport.WebAPI.DataModels;
using TechSupport.WebAPI.Infrastructure.Mapping;

namespace TechSupport.WebAPI.Api.Administration.DataModels
{
    public class UserProfileDataModel : AdministrationDataModel, IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
    }
}