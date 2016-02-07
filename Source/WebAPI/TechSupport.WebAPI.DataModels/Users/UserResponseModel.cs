namespace TechSupport.WebAPI.DataModels.Users
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using TechSupport.Data.Models;
    using TechSupport.WebApi.Common.Mapping;

    public class UserResponseModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserResponseModel>()
                .ForMember(m => m.Email, opt => opt.MapFrom(c => c.Email.ToString()))
                .ForMember(m => m.UserName, opt => opt.MapFrom(c => c.UserName.ToString()));
        }
    }
}
