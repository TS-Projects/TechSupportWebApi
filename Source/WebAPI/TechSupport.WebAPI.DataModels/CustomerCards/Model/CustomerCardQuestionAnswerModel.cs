namespace TechSupport.WebAPI.DataModels.CustomerCards.Model
{
    using System.ComponentModel.DataAnnotations;

    public class CustomerCardQuestionAnswerModel
    {
        public int QuestionId { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}
