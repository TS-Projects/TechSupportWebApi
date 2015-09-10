//using System.Data.Entity;
//using System.Linq;
//using System.Text.RegularExpressions;
//using System.Web;
//using System.Web.Http;
//using System.Web.Http.Cors;
//using System.Web.UI.WebControls;
//using TechSupport.Data;
//using TechSupport.Data.Models;
//using TechSupport.WebAPI.Api.CustomerCards.DataModels;
//using TechSupport.WebAPI.Api.CustomerCards.Models;
//using TechSupport.WebAPI.Controllers;

//namespace TechSupport.WebAPI.Api.CustomerCards.Controllers
//{
//    [EnableCors("*", "*", "*")]
//    public class CustomerCardRegistrationController : BaseApiController
//    {
//        public const string CompeteUrl = "Compete";
//        public const string PracticeUrl = "Practice";

//        public CustomerCardRegistrationController(ITechSupportData data)
//            : base(data)
//        {
//        }

//        public CustomerCardRegistrationController(ITechSupportData data, User userProfile)
//            : base(data, userProfile)
//        {
//        }

//        [NonAction]
//        public static void ValidateContest(Data.Models.CustomerCard contest, bool official)
//        {
//            if (contest == null || contest.IsDeleted || !contest.IsVisible)
//            {
//                throw new HttpException();
//            }

//            if (official && !contest.HasCustomerCardPassword)
//            {
//                throw new HttpException();
//            }
//        }

//        /// <summary>
//        /// Displays user compete information: tasks, send source form, ranking, submissions, ranking, etc.
//        /// Users only.
//        /// </summary>
//        [Authorize]
//        public IHttpActionResult Get(int id, bool official)
//        {
//            var contest = this.Data.CustomerCards.GetById(id);
//            ValidateContest(contest, official);

//            var participantFound = this.Data.Customers.Any(id, UserProfile.Id, official);

//            if (!participantFound)
//            {
//                if (!contest.ShouldShowRegistrationForm(official))
//                {
//                    this.Data.Customers.Add(new Customer(id, this.UserProfile.Id, official));
//                    this.Data.SaveChanges();
//                }
//                else
//                {
//                    // Participant not found, the contest requires password or the contest has questions
//                    // to be answered before registration. Redirect to the registration page.
//                    // The registration page will take care of all security checks.
//                    return this.Register(id, official);
//                }
//            }

//            var participant = this.Data.Customers.GetWithContest(id, this.UserProfile.Id, official);
//            var participantViewModel = new CustomerDataModel(participant, official);

//            return this.Ok(participantViewModel);
//        }

//        /// <summary>
//        /// Displays form for contest registration.
//        /// Users only.
//        /// </summary>
//        [HttpGet, Authorize]
//        public IHttpActionResult Register(int id, bool official)
//        {
//            var participantFound = this.Data.Customers.Any(id, this.UserProfile.Id, official);
//            if (participantFound)
//            {
//                // Participant exists. Redirect to index page.
//                return this.Get(id, official);
//            }

//            var contest = this.Data.CustomerCards.All().Include(x => x.Questions).FirstOrDefault(x => x.Id == id);

//            ValidateContest(contest, official);

//            if (contest.ShouldShowRegistrationForm(official))
//            {
//                var contestRegistrationModel = new CustomerCardRegistrationDataModel(contest, official);
//                return this.Ok(contestRegistrationModel);
//            }

//            var customer = new Customer(id, this.UserProfile.Id, official);
//            this.Data.Customers.Add(customer);
//            this.Data.SaveChanges();

//            return this.Get(id, official);
//        }

//        /// <summary>
//        /// Accepts form input for contest registration.
//        /// Users only.
//        /// </summary>
//        //// TODO: Refactor
//        [HttpPost, Authorize]
//        public IHttpActionResult Register(bool official, CustomerCardRegistrationModel registrationData)
//        {
//            // check if the user has already registered for participation and redirect him to the correct action
//            var participantFound = this.Data.Customers.Any(registrationData.ContestId, this.UserProfile.Id, official);

//            if (participantFound)
//            {
//                return this.Get(registrationData.ContestId, official);
//            }

//            var contest = this.Data.CustomerCards.GetById(registrationData.ContestId);
//            ValidateContest(contest, official);

//            if (official && contest.HasCustomerCardPassword)
//            {
//                if (string.IsNullOrEmpty(registrationData.Password))
//                {
//                    return this.BadRequest("Password");
//                }
//                else if (contest.CustomerCardPassword != registrationData.Password)
//                {
//                    return this.BadRequest("Password");
//                }
//            }

//            var questionsToAnswerCount = official ?
//                contest.Questions.Count(x => x.AskOfficialCustomers) :
//                contest.Questions.Count(x => x.AskPracticeCustomers);

//            if (questionsToAnswerCount != registrationData.Questions.Count())
//            {
//                return this.BadRequest("Questions");
//            }

//            var contestQuestions = contest.Questions.ToList();

//            var participant = new Customer(registrationData.ContestId, this.UserProfile.Id, official);
//            this.Data.Customers.Add(participant);
//            var counter = 0;
//            foreach (var question in registrationData.Questions)
//            {
//                var contestQuestion = contestQuestions.FirstOrDefault(x => x.Id == question.QuestionId);

//                var regularExpression = contestQuestion.RegularExpressionValidation;
//                bool correctlyAnswered = false;

//                if (!string.IsNullOrEmpty(regularExpression))
//                {
//                    correctlyAnswered = Regex.IsMatch(question.Answer, regularExpression);
//                }

//                participant.Answers.Add(new CustomerAnswer
//                {
//                    CustomerCardQuestionId = question.QuestionId,
//                    Answer = question.Answer
//                });

//                counter++;
//            }

//            if (!this.ModelState.IsValid)
//            {
//                return this.Ok(new CustomerCardRegistrationDataModel(contest, registrationData, official));
//            }

//            this.Data.SaveChanges();

//            return this.Get(registrationData.ContestId, official);
//        }
//    }
//}