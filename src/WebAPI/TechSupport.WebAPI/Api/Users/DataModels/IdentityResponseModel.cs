namespace TechSupport.WebAPI.Api.Users.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Microsoft.AspNet.Identity.EntityFramework;

    using TechSupport.Data.Models;
    using TechSupport.WebAPI.Api.Administration.DataModels;
    using TechSupport.WebAPI.Infrastructure.Mapping;

    public class IdentityResponseModel : IMapFrom<User>, IMapCustom
    {
        public string UserName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, IdentityResponseModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(c => c.UserName.ToString()));
        }
    }
}