using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TechSupport.Data.Models;
using TechSupport.WebApi.Common.Mapping;

namespace TechSupport.WebAPI.DataModels.Users
{
    public class CustomerCardRegistrationResponseModel : IMapFrom<CustomerCard>, IHaveCustomMappings
    {
        //public CustomerCardRegistrationResponseModel(CustomerCard card)
        //{
        //    this.ContestName = card.Name;
        //    this.ContestId = card.Id;

        //    this.RequirePassword = card.HasCustomerCardPassword;

        //}
         public string Id { get; set; }

         //public string ContestName { get; set; }

         //public bool RequirePassword { get; set; }

        public string CustomerCardPassword { get; set; }

         public void CreateMappings(IConfiguration configuration)
        {
            //configuration.CreateMap<CustomerCardRegistrationRequestModel, CustomerCard>()
            //    .ForMember(x => x.CustomerCardPassword, opt => opt.Ignore());
            //configuration.CreateMap<CustomerCard, CustomerCardRegistrationRequestModel>()
            //    .ForMember(m => m.UserName, opt => opt.MapFrom(c => c.User.UserName.ToString()))
            //    .ForMember(m => m.Password, opt => opt.MapFrom(c => c.CustomerCardPassword));
        }
    }
}
