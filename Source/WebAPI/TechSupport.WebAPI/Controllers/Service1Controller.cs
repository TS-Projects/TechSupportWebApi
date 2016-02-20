using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using TechSupport.Data;
using TechSupport.Data.Common.Repositories;
using TechSupport.Data.Models;
using TechSupport.Services.Data.Contracts;
using TechSupport.WebAPI.Api.CustomerCards.Controllers;
using TechSupport.WebAPI.DataModels.Users;

namespace TechSupport.WebAPI.Controllers
{
    public class Service1Controller : ApiController
    {
        private readonly ICustomerCardsRepository customerCard;

        public Service1Controller( ICustomerCardsRepository customerCard)
        {
            this.customerCard = customerCard;
        }

        /// <summary>
        /// Displays user compete information: tasks, send source form, ranking, submissions, ranking, etc.
        /// Users only.
        /// </summary>
        [HttpGet]
        [Authorize]
        public IHttpActionResult Index(string password, bool isAllowed)
        {
            var customerCard = this.customerCard.All().FirstOrDefault(p => p.CustomerCardPassword == password);
            //     ValidateContest(contest, official);

            var customerCardFound = this.customerCard.Any(password, this.User.Identity.GetUserId(), isAllowed);

            if (!customerCardFound)
            {
                if (!customerCard.ShouldShowRegistrationForm(isAllowed))
                {
                    this.customerCard.Add(new CustomerCard(password, this.User.Identity.GetUserId(), isAllowed));
                    this.customerCard.SaveChanges();
                }
                else
                {
                    // Participant not found, the contest requires password or the contest has questions
                    // to be answered before registration. Redirect to the registration page.
                    // The registration page will take care of all security checks.
                    return this.RedirectToRoute("Register", new { password, isAllowed });
                }
            }

            var participant = this.customerCard.GetWithContest(password, this.User.Identity.GetUserId(), isAllowed);
            //  var participantViewModel = new CustomerCardViewModel(participant, official);


            //      return this.OK(participantViewModel);
            return this.Ok();
        }

        /// <summary>
        /// Displays form for contest registration.
        /// Users only.
        /// </summary>
        [HttpGet, Authorize]
        public IHttpActionResult Register(string password, bool isAllowed)
        {
            var customerCardFound = this.customerCard.Any(password, this.User.Identity.GetUserId(), isAllowed);

            if (customerCardFound)
            {
                // Participant exists. Redirect to index page.
                return this.RedirectToRoute( "Index", new { password, isAllowed });
            }

            var card = new CustomerCard(password, this.User.Identity.GetUserId(), isAllowed);
            this.customerCard.Add(card);
            this.customerCard.SaveChanges();

            return this.RedirectToRoute("Index", new { password, isAllowed });
        }

        /// <summary>
        /// Accepts form input for contest registration.
        /// Users only.
        /// </summary>
        //// TODO: Refactor
        [HttpPost, Authorize]
        public IHttpActionResult Register(bool isAllowed, CustomerCardRegistrationModel registrationData)
        {
            // check if the user has already registered for participation and redirect him to the correct action

            var customerCardFound = this.customerCard.Any(registrationData.Password, this.User.Identity.GetUserId(), isAllowed);

            if (customerCardFound)
            {
                return this.RedirectToRoute( "Index", new { id = registrationData.Password, isAllowed });
            }

            var customerCard = this.customerCard.GetById(registrationData.Id);

        //    ValidateContest(contest, official);

            if (isAllowed && customerCard.HasCustomerCardPassword)
            {
                if (string.IsNullOrEmpty(registrationData.Password))
                {
                 //   this.ModelState.AddModelError("Password", Resource.Views.CompeteRegister.Empty_Password);
                }
                else if (customerCard.CustomerCardPassword != registrationData.Password)
                {
                //    this.ModelState.AddModelError("Password", Resource.Views.CompeteRegister.Incorrect_password);
                }
            }

            if (!isAllowed && customerCard.HasCustomerCardPassword)
            {
                if (string.IsNullOrEmpty(registrationData.Password))
                {
          //          this.ModelState.AddModelError("Password", Resource.Views.CompeteRegister.Empty_Password);
                }
                else if (customerCard.CustomerCardPassword != registrationData.Password)
                {
              //      this.ModelState.AddModelError("Password", Resource.Views.CompeteRegister.Incorrect_password);
                }
            }

            var card = new CustomerCard(registrationData.Id, this.User.Identity.GetUserId(), isAllowed);
            this.customerCard.Add(card);

            if (!this.ModelState.IsValid)
            {

          //      return this.View(new ContestRegistrationViewModel(contest, registrationData, official));
            }

            this.customerCard.SaveChanges();

            return this.RedirectToRoute("Index", new { id = registrationData.Password, isAllowed });
        }

    }
}