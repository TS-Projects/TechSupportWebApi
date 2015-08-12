using System.ComponentModel;
using System.Web.Mvc;
using TechSupport.Data.Models;
using TechSupport.WebAPI.DataModels;
using TechSupport.WebAPI.Infrastructure.Mapping;

namespace TechSupport.WebAPI.Api.CustomerCards.DataModels
{
    public class CustomerCardCategoryDataModel : AdministrationDataModel, IMapFrom<CustomerCardCategory>
    {
        [DefaultValue(null)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsVisible { get; set; }
    }
}