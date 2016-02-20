using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using TechSupport.Data.Common.Repositories;
using TechSupport.Data.Models;
using TechSupport.WebAPI.Api.CustomerCards.Controllers;
using TechSupport.WebAPI.DataModels.Users;

namespace TechSupport.WebAPI.Controllers
{
    public class ServiceController : ApiController
    {
        private readonly IRepository<CustomerCard> customerCards;

        public ServiceController(IRepository<CustomerCard> customerCards)
        {
            this.customerCards = customerCards;
        }

        public IHttpActionResult Get(string id, bool isAllowed)
        {
            var customerCard = this.customerCards.All().FirstOrDefault(p => p.Id == id);

            var customerCardFound = this.customerCards.All().Any(p => p.Id == id && p.UserId == this.User.Identity.GetUserId() && isAllowed);
            //     ValidateContest(contest, official);

            if (!customerCardFound)
            {
                if (!customerCard.ShouldShowRegistrationForm(isAllowed))
                {
                    this.customerCards.Add(new CustomerCard(id, this.User.Identity.GetUserId(), isAllowed));
                    this.customerCards.SaveChanges();
                }
                else
                {
                    // Participant not found, the contest requires password or the contest has questions
                    // to be answered before registration. Redirect to the registration page.
                    // The registration page will take care of all security checks.
                    return this.RedirectToRoute("Register", new { id, isAllowed });
                }
            }

            var participant = this.customerCards.All().FirstOrDefault(p => p.Id == id && p.UserId == this.User.Identity.GetUserId() && isAllowed);
            //  var participantViewModel = new CustomerCardViewModel(participant, official);
            //var customerCardViewModel = 

            //      return this.OK(participantViewModel);
            return this.Ok();
        }

        public IHttpActionResult Post(bool isAllowed, CustomerCardRegistrationModel model)
        {


            var customerCardFound = this.customerCards.All().Any(p => p.Id == model.Id && p.UserId == this.User.Identity.GetUserId() && isAllowed);

            if (customerCardFound == null)
            {
                return NotFound();
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

            return this.Get(model.Id, isAllowed);
        }

        [HttpGet, Authorize]
        public IHttpActionResult Register(string id, bool isAllowed)
        {
            var customerCardFound = this.customerCards.All().Any(p => p.Id == id && p.UserId == this.User.Identity.GetUserId() && isAllowed);


            if (customerCardFound)
            {
                // Participant exists. Redirect to index page.
                return this.RedirectToRoute("Index", new { password, isAllowed });
            }

            var card = new CustomerCard(id, this.User.Identity.GetUserId(), isAllowed);
            this.customerCards.Add(card);
            this.customerCards.SaveChanges();

            return this.RedirectToRoute("Index", new { password, isAllowed });
        }

    }
}