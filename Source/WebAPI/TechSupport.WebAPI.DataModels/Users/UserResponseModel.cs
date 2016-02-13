using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TechSupport.Data.Models;
using TechSupport.WebApi.Common.Mapping;

namespace TechSupport.WebAPI.DataModels.Users
{
    public class UserResponseModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserResponseModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(c => c.UserName.ToString()))
                .ForMember(m => m.Email, opt => opt.MapFrom(c => c.Email.ToString()));
        }
    }
}
