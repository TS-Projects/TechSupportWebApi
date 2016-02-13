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
    public class UserNameModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string UserName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserResponseModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(c => c.UserName.ToString()));
        }
    }
}
