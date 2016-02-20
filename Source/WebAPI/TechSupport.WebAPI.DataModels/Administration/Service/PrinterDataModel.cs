using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.WebAPI.DataModels.Administration.Service
{
    publc class PrinterDataModel
    {

        [DatabaseProperty]
        [Display(Name = "№")]
        [DefaultValue(null)]
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [DatabaseProperty]
        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [Required(
            ErrorMessageResourceName = "Name_required",
            ErrorMessageResourceType = typeof(Resource))]
        [StringLength(
            GlobalConstants.ContestCategoryNameMaxLength,
            MinimumLength = GlobalConstants.ContestCategoryNameMinLength,
            ErrorMessageResourceName = "Name_length",
            ErrorMessageResourceType = typeof(Resource))]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [DatabaseProperty]
        [Display(Name = "Order_by", ResourceType = typeof(Resource))]
        [Required(
            ErrorMessageResourceName = "Order_by_required",
            ErrorMessageResourceType = typeof(Resource))]
        [UIHint("Integer")]
        public int OrderBy { get; set; }

        [DatabaseProperty]
        [Display(Name = "Visibility", ResourceType = typeof(Resource))]
        public bool IsVisible { get; set; }
    }
}
