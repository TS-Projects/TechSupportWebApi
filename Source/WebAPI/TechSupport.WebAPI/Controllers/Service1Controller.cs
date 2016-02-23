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
    public class ServiceController1 : ApiController
    {
        private readonly IRepository<CustomerCard> customerCards;

        public ServiceController1(IRepository<CustomerCard> customerCards)
        {
            this.customerCards = customerCards;
        }

        [HttpPost]
        public IHttpActionResult ValidateCaptcha()
        {
            //var response = Request["g-recaptcha-response"];
            ////secret that was generated in key value pair
            //const string secret = "YOUR KEY VALUE PAIR";

            //var client = new WebClient();
            //var reply =
            //    client.DownloadString(
            //        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            //var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            ////when response is false check for the error message
            //if (!captchaResponse.Success)
            //{
            //    if (captchaResponse.ErrorCodes.Count <= 0) return View();

            //    var error = captchaResponse.ErrorCodes[0].ToLower();
            //    switch (error)
            //    {
            //        case ("missing-input-secret"):
            //            ViewBag.Message = "The secret parameter is missing.";
            //            break;
            //        case ("invalid-input-secret"):
            //            ViewBag.Message = "The secret parameter is invalid or malformed.";
            //            break;

            //        case ("missing-input-response"):
            //            ViewBag.Message = "The response parameter is missing.";
            //            break;
            //        case ("invalid-input-response"):
            //            ViewBag.Message = "The response parameter is invalid or malformed.";
            //            break;

            //        default:
            //            ViewBag.Message = "Error occured. Please try again";
            //            break;
            //    }
            //}
            //else
            //{
            //    ViewBag.Message = "Valid";
            //}

            //return View();
            return this.Ok();           
        }
    }
}