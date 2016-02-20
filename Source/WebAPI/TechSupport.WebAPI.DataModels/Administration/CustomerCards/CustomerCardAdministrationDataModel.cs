using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TechSupport.Data.Models;
using TechSupport.WebApi.Common.Mapping;

namespace TechSupport.WebAPI.DataModels.Administration.CustomerCards
{
    class CustomerCardAdministrationDataModel : IMapFrom<CustomerCard>, IHaveCustomMappings
    {
        [DefaultValue(null)]
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public bool Informed { get; set; }

        public bool Warranty { get; set; }

        public string ContestPassword { get; set; }

        public string PracticePassword { get; set; }

        public string Description { get; set; }

        public int OrderBy { get; set; }

        public bool IsVisible { get; set; }

        [DefaultValue(null)]
        public int? CategoryId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<CustomerCard, CustomerCardAdministrationDataModel>()
                .ForMember(m => m.CategoryId, opt => opt.MapFrom(c => c.CategoryId.Value));
        }
    }
}
