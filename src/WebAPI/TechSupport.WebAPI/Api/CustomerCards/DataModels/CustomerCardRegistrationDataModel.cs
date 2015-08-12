using AutoMapper;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Linq;
using TechSupport.WebAPI.Api.CustomerCards.Models;
using TechSupport.WebAPI.DataModels;
using Model = TechSupport.Data.Models.CustomerCard;

namespace TechSupport.WebAPI.Api.CustomerCards.DataModels
{
    public class CustomerCardRegistrationDataModel : AdministrationDataModel
    {
        //IGeneralData regData = new GeneralData();
        public CustomerCardRegistrationDataModel(Model contest, bool isOfficial)
        {
            this.ContestId = contest.Id;

            if (isOfficial)
            {
                this.RequirePassword = contest.HasCustomerCardPassword;
            }

            //var data = this.regData.Data
            // .CustomerCardQuestions
            // .All()
            // .AsQueryable()
            // .Project()
            // .To<CustomerCardQuestionDataModel>();

            this.Questions = contest.Questions.AsQueryable().Select(c => Mapper.Map<Model, CustomerCardQuestionDataModel>(contest));
        }

        public CustomerCardRegistrationDataModel(Model contest, CustomerCardRegistrationModel userAnswers, bool isOfficial)
            : this(contest, isOfficial)
        {
            this.Questions = this.Questions.Select(x =>
            {
                var userAnswer = userAnswers.Questions.FirstOrDefault(y => y.QuestionId == x.Id);
                return new CustomerCardQuestionDataModel
                {
                    //   Answer = userAnswer == null ? null : userAnswer.Answer,
                    Id = x.Id,
                    Text = x.Text
                    //  Question = x.Question,
                    //   Type = x.Type,
                    //    PossibleAnswers = x.PossibleAnswers
                };
            });
        }

        public int ContestId { get; set; }

        public string ContestName { get; set; }

        public bool RequirePassword { get; set; }

        public string Password { get; set; }

        public IEnumerable<CustomerCardQuestionDataModel> Questions { get; set; }
    }
}