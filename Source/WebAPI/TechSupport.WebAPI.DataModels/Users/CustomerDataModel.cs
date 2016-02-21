namespace TechSupport.WebAPI.DataModels.Administration.CustomerCards
{
    using System;
    using System.ComponentModel;
    using AutoMapper;
    using TechSupport.Data.Models;
    using TechSupport.WebApi.Common.Mapping;

    public class CustomerDataModel : IMapFrom<CustomerCard>, IHaveCustomMappings
    {
        //[DefaultValue(null)]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<CustomerCard, CustomerCardAdministrationDataModel>()
                .ForMember(m => m.CategoryId, opt => opt.MapFrom(c => c.CategoryId.Value));
        }
    }
}
