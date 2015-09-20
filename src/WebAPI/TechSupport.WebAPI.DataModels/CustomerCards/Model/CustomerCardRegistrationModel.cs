//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TechSupport.WebAPI.DataModels.CustomerCards.Model
//{
//    public class CustomerCardRegistrationModel : IValidatableObject
//    {
//        private ITechSupportData regData;

    //        public CustomerCardRegistrationModel() : this(new TechSupportData())
    //        {
    //        }

    //        public CustomerCardRegistrationModel(ITechSupportData regData)
    //        {
    //            this.Questions = new HashSet<CustomerCardQuestionAnswerModel>();
    //            this.regData = regData;
    //        }

    //        public int ContestId { get; set; }

    //        public string Password { get; set; }

    //        public IEnumerable<CustomerCardQuestionAnswerModel> Questions { get; set; }

    //        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //        {
    //            var validationResults = new HashSet<ValidationResult>();

    //            var contest = this.regData.CustomerCards.GetById(this.ContestId);
    //            var contestQuestions = contest.Questions.ToList();

    //            var counter = 0;
    //            foreach (var question in contestQuestions)
    //            {
    //                var answer = this.Questions.FirstOrDefault(x => x.QuestionId == question.Id);
    //                var memberName = string.Format("Questions[{0}].Answer", counter);
    //                if (answer == null)
    //                {
    //                    var validationErrorMessage = string.Format("Question with id {0} was not answered.", question.Id);
    //                    var validationResult = new ValidationResult(validationErrorMessage, new[] { memberName });
    //                    validationResults.Add(validationResult);
    //                }
    //                else if (!string.IsNullOrWhiteSpace(question.RegularExpressionValidation) &&
    //                         !Regex.IsMatch(answer.Answer, question.RegularExpressionValidation))
    //                {
    //                    var validationErrorMessage = string.Format(
    //                        "Question with id {0} is not in the correct format.", question.Id);
    //                    var validationResult = new ValidationResult(validationErrorMessage, new[] { memberName });
    //                    validationResults.Add(validationResult);
    //                }

    //                counter++;
    //            }

    //            return validationResults;
    //        }
    //    }
//}
