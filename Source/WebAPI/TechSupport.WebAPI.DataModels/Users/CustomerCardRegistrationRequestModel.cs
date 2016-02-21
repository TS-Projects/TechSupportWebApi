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
    public class CustomerCardRegistrationRequestModel : IMapFrom<CustomerCard>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<CustomerCard, CustomerCardRegistrationRequestModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(c => c.User.UserName.ToString()))
                .ForMember(m => m.Password, opt => opt.MapFrom(c => c.CustomerCardPassword));
        }
    }
}
