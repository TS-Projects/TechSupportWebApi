using System;

namespace TechSupport.WebAPI.DataModels
{
    public abstract class AdministrationDataModel
    {
        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}