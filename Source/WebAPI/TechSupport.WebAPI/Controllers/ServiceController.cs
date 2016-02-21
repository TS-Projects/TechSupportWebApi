using System;
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

            var model = this.customerCards
                 .All()
                 .Where(p => p.UserId == userId)
                 .ProjectTo<CustomerDataModel>()
                 .ToList();

            return this.Data(model);
        }

        /// <summary>
        /// Accepts form input for contest registration.
        /// Users only.
        /// </summary>
        [HttpPost, Authorize]
        public IHttpActionResult Post(OrderRequestDataModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = this.customerCards.All().FirstOrDefault(p => p.Id == model.Id);

            if (order == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Order number not exist!");
            }

            var userId = this.User.Identity.GetUserId();

            var isExistUserWithOrder = this.customerCards.All().Any(p => p.Id == model.Id && p.UserId == userId);

            if (isExistUserWithOrder)
            {
                return this.Get(model.Id);
            }

            order.UserId = userId;

            this.customerCards.Update(order);

            this.customerCards.SaveChanges();

            return this.Get(model.Id);
        }
    }
}