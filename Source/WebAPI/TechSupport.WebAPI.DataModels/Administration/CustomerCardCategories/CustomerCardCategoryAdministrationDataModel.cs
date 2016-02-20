namespace TechSupport.WebAPI.DataModels.Administration.CustomerCardCategories
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using TechSupport.WebApi.Common.Mapping;
    using TechSupport.WebAPI.Common;
    using TechSupport.Data.Models;

    class CustomerCardCategoryDataModel : IMapFrom<CustomerCardCategory>
    {
        [DefaultValue(null)]
        public int? Id { get; set; }

        [Required]
        [StringLength(
            Constants.CustomerCardCategoryNameMinLength,
            MinimumLength = Constants.CustomerCardCategoryNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public int OrderBy { get; set; }

        public bool IsVisible { get; set; }
    }
}
