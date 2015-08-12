using System;
using TechSupport.Data.Models;
using TechSupport.WebAPI.DataModels;
using TechSupport.WebAPI.Infrastructure.Mapping;

namespace TechSupport.WebAPI.Api.Administration.DataModels
{
    public class CustomerCardAdministrationDataModel : AdministrationDataModel, IMapFrom<Customer>
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

        public string CustomerCardPassword { get; set; }

        public int CustomerId { get; set; }
    }
}