using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
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
        /// Validates if a contest is correctly found. If the user wants to practice or compete in the contest
        /// checks if the contest can be practiced or competed.
        /// </summary>
        /// <param name="contest">Contest to validate.</param>
        /// <param name="official">A flag checking if the contest will be practiced or competed</param>
        [NonAction]
        public static void ValidateContest(CustomerCard card)
        {
            if (card == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Invalid contest id was provided!");
            }
        }

        /// <summary>
        /// Displays user compete information: tasks, send source form, ranking, submissions, ranking, etc.
        /// Users only.
        /// </summary>
        [Authorize]
        public IHttpActionResult Get(string id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var customerCard = this.customerCards.All().FirstOrDefault(p => p.Id == id);

            var customerCardFound = this.customerCards.All().Any(p => p.Id == id && p.UserId == currentUserId);
            ValidateContest(customerCard);

            if (!customerCardFound)
            {
                if (!customerCard.ShouldShowRegistrationForm())
                {
                    if (customerCard == null)
                    {
                        return NotFound();
                    }
                    //Password at user already set in customerCard
                    //Add currentUserId to current CustomerCard and then display card
                    customerCard.UserId = currentUserId;
                    this.customerCards.Update(customerCard);
                  //  this.customerCards.Add(new CustomerCard(id, this.User.Identity.GetUserId()));
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

        //    var participant = this.customerCards.All().FirstOrDefault(p => p.Id == id && p.UserId == currentUserId);
            //  var participantViewModel = new CustomerCardViewModel(participant, official);
            //var customerCardViewModel = 

            //var userId = this.User.Identity.GetUserId();
            //var currUser  = this.
            ////      return this.OK(participantViewModel);
            //return this.Ok();

            var model = this.customerCards
                 .All()
                 .Where(u => u.Id == id && u.UserId == currentUserId)
                 .ProjectTo<CustomerDataModel>()
                 .FirstOrDefault();

            return this.Data(model);
        }

        /// <summary>
        /// Displays form for contest registration.
        /// Users only.
        /// </summary>
        [HttpGet, Authorize]
        public IHttpActionResult Post(string id)
        {
            var currentUserId = this.User.Identity.GetUserId();

            var customerCardFound = this.customerCards.All().Any(p => p.Id == id && p.UserId == currentUserId);


            if (customerCardFound)
            {
                // Participant exists. Redirect to index page.
                return this.Get(id);
            }

            var customerCard = this.customerCards.All().FirstOrDefault(x => x.Id == id);

            ValidateContest(customerCard);

            if (customerCard.ShouldShowRegistrationForm())
            {
                //var contestRegistrationModel = new ContestRegistrationViewModel(contest, official);

                customerCard.UserId = currentUserId;
                this.customerCards.Add(customerCard);

                var model = this.customerCards
                     .All()
                     .Where(u => u.Id == id)
                     .ProjectTo<CustomerCardRegistrationResponseModel>()
                     .FirstOrDefault();

                return this.Ok(model);
            }

            var card = this.customerCards.All().FirstOrDefault(p => p.Id == id);
            if (card == null)
            {
                return NotFound();
            }
            card.UserId = currentUserId;
            this.customerCards.Update(card);
            this.customerCards.SaveChanges();

            //var card = new CustomerCard(id, this.User.Identity.GetUserId());
            //this.customerCards.Add(card);
            //this.customerCards.SaveChanges();

            return this.Get(id);
        }

        /// <summary>
        /// Accepts form input for contest registration.
        /// Users only.
        /// </summary>
        //// TODO: Refactor
        [HttpPost, Authorize]
        public IHttpActionResult Post(CustomerCardRegistrationRequestModel model)
        {
            var currentUserId = this.User.Identity.GetUserId();

            var customerCardFound = this.customerCards.All().Any(p => p.Id == model.Id && p.UserId == currentUserId);

            if (customerCardFound)
            {
                return this.Get(model.Id);
            }

            var customerCard = this.customerCards.GetById(model.Id);
            ValidateContest(customerCard);

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

            customerCard.UserId = currentUserId;

            this.customerCards.Update(customerCard);

            if (!ModelState.IsValid)
            {
                customerCard.UserId = currentUserId;
                this.customerCards.Add(customerCard);

                var card = this.customerCards
                     .All()
                     .Where(u => u.Id == model.Id)
                     .ProjectTo<CustomerCardRegistrationResponseModel>()
                     .FirstOrDefault();

                return this.Ok(card);
               // return this.View(new ContestRegistrationViewModel(contest, registrationData, official));
            }

            this.customerCards.SaveChanges();

            return this.Get(model.Id);
        }
    }
}