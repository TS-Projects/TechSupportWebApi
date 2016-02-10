namespace TechSupport.WebAPI.DataModels.Users
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using TechSupport.Data.Models;
    using TechSupport.WebApi.Common.Mapping;

    public class IdentityResponseModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string IsAdmin { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, IdentityResponseModel>()
                .ForMember(m => m.Email, opt => opt.MapFrom(c => c.Email.ToString()))
                .ForMember(m => m.UserName, opt => opt.MapFrom(c => c.UserName.ToString()));
        }
    }
}
