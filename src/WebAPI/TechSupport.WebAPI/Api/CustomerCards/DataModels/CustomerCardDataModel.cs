using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TechSupport.Data.Models;
using TechSupport.WebAPI.DataModels;
using TechSupport.WebAPI.Infrastructure.Mapping;

namespace TechSupport.WebAPI.Api.CustomerCards.DataModels
{
    public class CustomerCardDataModel : AdministrationDataModel, IMapFrom<CustomerCard>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public bool IsVisible { get; set; }

        public int? CategoryId { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public string Summary { get; set; }

        public decimal Price { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Informed { get; set; }

        public bool Warranty { get; set; }

        public int CustomerId { get; set; }

        public IEnumerable<CustomerCardQuestionDataModel> Questions { get; set; }

        public string CustomerCardPassword { get; set; }

        public bool HasCustomerCardPassword
        {
            get { return this.CustomerCardPassword != null; }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Data.Models.CustomerCard, CustomerCardDataModel>()
                .ForMember(m => m.Questions, opt => opt.MapFrom(u => u.Questions.OrderByDescending(c => c.CreatedOn).Take(5)));
        }
    }
}