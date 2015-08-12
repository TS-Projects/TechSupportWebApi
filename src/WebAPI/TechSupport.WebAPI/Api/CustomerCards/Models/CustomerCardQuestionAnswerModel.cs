using System.ComponentModel.DataAnnotations;

namespace TechSupport.WebAPI.Api.CustomerCards.Models
{
    public class CustomerCardQuestionAnswerModel
    {
        public int QuestionId { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}