namespace TechSupport.WebAPI.DataModels.Administration.CustomerCards
{
    using System;
    using System.ComponentModel;
    using AutoMapper;
    using TechSupport.Data.Models;
    using TechSupport.WebApi.Common.Mapping;

    public class CustomerCardAdministrationDataModel : IMapFrom<CustomerCard>, IHaveCustomMappings
    {
        //[DefaultValue(null)]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public bool Informed { get; set; }

        public bool Warranty { get; set; }

        public string CustomerCardPassword { get; set; }

        public string Description { get; set; }

        public bool IsVisible { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        public DateTime? EndDate { get; set; }

        [DefaultValue(null)]
        public int? CategoryId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<CustomerCard, CustomerCardAdministrationDataModel>()
                .ForMember(m => m.CategoryId, opt => opt.MapFrom(c => c.CategoryId.Value));
        }
    }
}
