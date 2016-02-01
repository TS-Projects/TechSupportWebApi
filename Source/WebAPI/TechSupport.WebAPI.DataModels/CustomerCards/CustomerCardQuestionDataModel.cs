using System;
using System.Collections.Generic;
namespace TechSupport.WebAPI.DataModels.CustomerCards
{
    using TechSupport.Data.Models;
    using TechSupport.WebApi.Common.Mapping;

    public class CustomerCardQuestionDataModel : IMapFrom<CustomerCardQuestion>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string RegularExpressionValidation { get; set; }
    }
}
