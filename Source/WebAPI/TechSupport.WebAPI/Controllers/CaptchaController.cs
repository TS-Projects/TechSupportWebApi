using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TechSupport.Data.Common.Repositories;
using TechSupport.Data.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Reflection.Emit;
using TechSupport.WebAPI.Api.CustomerCards.Controllers;
using TechSupport.WebAPI.DataModels;
using TechSupport.WebAPI.DataModels.Administration.CustomerCards;
using TechSupport.WebAPI.DataModels.Users;
using TechSupport.WebAPI.Infrastructure;
using TechSupport.WebAPI.Infrastructure.Extensions;

namespace TechSupport.WebAPI.Controllers
{
    public class CaptchaController : ApiController
    {
        private readonly IRepository<CustomerCard> customerCards;

        public CaptchaController(IRepository<CustomerCard> customerCards)
        {
            this.customerCards = customerCards;
        }

        public async Task<IHttpActionResult> Get()
        {
            return this.Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post()
        {
            var obj = await Request.Content.ReadAsAsync<JObject>();
            var model = obj.ToObject<CaptchaResponse>();
            var encodedResponse = model.GRecaptchaResponse;

            var isCaptchaValid = ReCaptcha.Validate(encodedResponse);

            if (!isCaptchaValid)
            {
                return this.BadRequest("Sorry mate, wrong captcha response. Are you a bot?");
            }

            return this.Ok();
        }
    }
}