using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using TechSupport.Data.Common.Repositories;
using TechSupport.Data.Models;
using TechSupport.WebAPI.Api.CustomerCards.Controllers;
using TechSupport.WebAPI.DataModels.Administration.CustomerCards;
using TechSupport.WebAPI.DataModels.Users;
using TechSupport.WebAPI.Infrastructure.Extensions;

namespace TechSupport.WebAPI.Controllers
{
    public class ServiceController : ApiController
    {
        private readonly IRepository<CustomerCard> customerCards;

        public ServiceController(IRepository<CustomerCard> customerCards)
        {
            this.customerCards = customerCards;
        }

        /// <summary>
        /// Displays user compete information: tasks, send source form, ranking, submissions, ranking, etc.
        /// Users only.
        /// </summary>
        [Authorize]
        public IHttpActionResult Get(string id)
        {
            var customerCard = this.customerCards.All().FirstOrDefault(p => p.Id == id);

            var customerCardFound = this.customerCards.All().Any(p => p.Id == id && p.UserId == this.User.Identity.GetUserId());
            //     ValidateContest(contest, official);

            if (!customerCardFound)
            {
                if (!customerCard.ShouldShowRegistrationForm())
                {
                    this.customerCards.Add(new CustomerCard(id, this.User.Identity.GetUserId()));
                    this.customerCards.SaveChanges();
                }
                else
                {
                    // Participant not found, the contest requires password or the contest has questions
                    // to be answered before registration. Redirect to the registration page.
                    // The registration page will take care of all security checks.
                    return this.Post(id);
                }
            }

            var participant = this.customerCards.All().FirstOrDefault(p => p.Id == id && p.UserId == this.User.Identity.GetUserId());
            //  var participantViewModel = new CustomerCardViewModel(participant, official);
            //var customerCardViewModel = 

            //var userId = this.User.Identity.GetUserId();
            //var currUser  = this.
            ////      return this.OK(participantViewModel);
            //return this.Ok();

            var model = this.customerCards
                 .All()
                 .Where(u => u.UserId == participant.Id)
                 .ProjectTo<CustomerDataModel>()
                 .FirstOrDefault();

            return this.Data(model);
        }

        /// <summary>
        /// Accepts form input for contest registration.
        /// Users only.
        /// </summary>
        //// TODO: Refactor
        [HttpPost, Authorize]
        public IHttpActionResult Post(CustomerCardRegistrationModel model)
        {
            var customerCardFound = this.customerCards.All().Any(p => p.Id == model.Id && p.UserId == this.User.Identity.GetUserId());

            if (customerCardFound)
            {
                return this.Get(model.Id);
            }

            var customerCard = this.customerCards.GetById(model.Id);

            if (customerCard.HasCustomerCardPassword)
            {
                if (string.IsNullOrEmpty(model.Password))
                {
                    BadRequest("Error 2");
                }
                else if (customerCard.CustomerCardPassword != model.Password)
                {
                    BadRequest("Error 3");
                }
            }

            customerCard.UserId = this.User.Identity.GetUserId();
            this.customerCards.Add(customerCard);

            if (!ModelState.IsValid)
            {
                return BadRequest("Error 4");
               // return this.View(new ContestRegistrationViewModel(contest, registrationData, official));
            }

            this.customerCards.SaveChanges();

            return this.Get(model.Id);
        }

        /// <summary>
        /// Displays form for contest registration.
        /// Users only.
        /// </summary>
        [HttpGet, Authorize]
        public IHttpActionResult Post(string id)
        {
            var customerCardFound = this.customerCards.All().Any(p => p.Id == id && p.UserId == this.User.Identity.GetUserId());


            if (customerCardFound)
            {
                // Participant exists. Redirect to index page.
                return this.Get(id);
            }

            //ValidateContest(contest, official);

            //if (contest.ShouldShowRegistrationForm(official))
            //{
            //    var contestRegistrationModel = new ContestRegistrationViewModel(contest, official);
            //    return this.View(contestRegistrationModel);
            //}

            var card = this.customerCards.All().FirstOrDefault(p => p.Id == id);
            card.UserId = this.User.Identity.GetUserId();
            this.customerCards.Add(card);
            this.customerCards.SaveChanges();

            //var card = new CustomerCard(id, this.User.Identity.GetUserId());
            //this.customerCards.Add(card);
            //this.customerCards.SaveChanges();

            return this.Get(id);
        }

    }
}