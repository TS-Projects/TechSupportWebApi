using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using TechSupport.Data.Common.Repositories;
using TechSupport.Data.Models;
using TechSupport.WebAPI.Api.CustomerCards.Controllers;
using TechSupport.WebAPI.DataModels;
using TechSupport.WebAPI.DataModels.Administration.CustomerCards;
using TechSupport.WebAPI.DataModels.Users;
using TechSupport.WebAPI.Infrastructure;
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

        [Authorize]
        public IHttpActionResult Get(string id)
        {


            var userId = this.User.Identity.GetUserId();
            var isExistOrder = this.customerCards.All().Any(p => p.Id == id && p.UserId == userId);

            if (!isExistOrder)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Order number for this user not exist!");
            }

            var model = this.customerCards
                 .All()
                 .Where(p => p.Id == id && p.UserId == userId)
                 .ProjectTo<CustomerDataModel>()
                 .FirstOrDefault();

            return this.Data(model);
        }

        [Authorize]
        public IHttpActionResult Get()
        {
            var userId = this.User.Identity.GetUserId();

            var existCard = this.customerCards.All().Any(p => p.UserId == userId);

            if (existCard)
            {
                var model = this.customerCards
                     .All()
                     .Where(p => p.UserId == userId)
                     .ProjectTo<CustomerDataModel>()
                     .ToList();

                return this.Ok(model);
            }

            return this.NotFound();
        }

        /// <summary>
        /// Accepts form input for contest registration.
        /// Users only.
        /// </summary>
        [HttpPost, Authorize]
        public IHttpActionResult Post(OrderRequestDataModel model)
        {
            //var obj = await Request.Content.ReadAsAsync<JObject>();
            //var modelCaptcha = obj.ToObject<CaptchaResponse>();
            //var encodedResponse = modelCaptcha.GRecaptchaResponse;
            var encodedResponse = model.GRecaptchaResponse;

            var isCaptchaValid = ReCaptcha.Validate(encodedResponse);

            if (!isCaptchaValid)
            {
                return this.BadRequest("Sorry mate, wrong captcha response. Are you a bot?");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = this.User.Identity.GetUserId();

            var isExistUserWithOrder = this.customerCards.All().Any(p => p.Id == model.Id && p.UserId == userId);

            if (isExistUserWithOrder)
            {
                return this.Get(model.Id);
            }

            var order = this.customerCards.All().FirstOrDefault(p => p.Id == model.Id);

            if (order == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Order number not exist!");
            }

            order.UserId = userId;

            this.customerCards.Update(order);

            this.customerCards.SaveChanges();

            return this.Get(model.Id);
        }
    }
}