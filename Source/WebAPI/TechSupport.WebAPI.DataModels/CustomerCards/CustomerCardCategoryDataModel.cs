namespace TechSupport.WebAPI.DataModels.CustomerCards
{
    using TechSupport.Data.Models;
    using TechSupport.WebApi.Common.Mapping;

    public class CustomerCardCategoryDataModel : IMapFrom<CustomerCardCategory>
    {
        // [DefaultValue(null)]
        // [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsVisible { get; set; }
    }
}
