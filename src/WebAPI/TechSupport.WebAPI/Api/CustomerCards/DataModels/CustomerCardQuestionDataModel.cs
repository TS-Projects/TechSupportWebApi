using TechSupport.Data.Models;
using TechSupport.WebAPI.DataModels;
using TechSupport.WebAPI.Infrastructure.Mapping;

namespace TechSupport.WebAPI.Api.CustomerCards.DataModels
{
    public class CustomerCardQuestionDataModel : AdministrationDataModel, IMapFrom<CustomerCardQuestion>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string RegularExpressionValidation { get; set; }
    }
}