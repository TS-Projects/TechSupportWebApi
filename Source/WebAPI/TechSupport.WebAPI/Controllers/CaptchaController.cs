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
using TechSupport.WebAPI.DataModels.Administration.CustomerCards;
using TechSupport.WebAPI.DataModels.Users;
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
            var model = obj.ToObject<FormModel>();
            var response = model.GRecaptchaResponse;
            //secret that was generated in key value pair
            const string secret = "6LeyHRkTAAAAAD-nRFFC3iK8pIggHj5-C7ZgFeKo";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0) return this.Ok();

                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        return this.BadRequest("The secret parameter is missing.");
                        break;
                    case ("invalid-input-secret"):
                        return this.BadRequest("The secret parameter is invalid or malformed.");
                        break;

                    case ("missing-input-response"):
                        return this.BadRequest("The response parameter is missing.");
                        break;
                    case ("invalid-input-response"):
                        return this.BadRequest("The response parameter is invalid or malformed.");
                        break;

                    default:
                        return this.BadRequest("Error occured. Please try again");
                        break;
                }
            }

            return this.Ok();
        }

        //public async Task<HttpResponseMessage> Post()
        //{
        //    var obj = await Request.Content.ReadAsAsync<JObject>();
        //    var model = obj.ToObject<FormModel>();
        //    var response = model.GRecaptchaResponse;

        //    var httpClient = new HttpClient();
        //    var PK = "6LeyHRkTAAAAAD-nRFFC3iK8pIggHj5-C7ZgFeKo";
        //    var userIP = ((HttpContextBase)this.Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
        //    var uri = "http://www.google.com/recaptcha/api/verify";

        //    var postData = new List<KeyValuePair<string, string>>();
        //    postData.Add(new KeyValuePair<string, string> ("privatekey", PK));
        //    postData.Add(new KeyValuePair<string, string>("remoteip", userIP));
        //    //postData.Add(new KeyValuePair<string, string>("challenge", data.Challenge));
        //    postData.Add(new KeyValuePair<string, string>("response", data.Response));

        //    HttpContent content = new FormUrlEncodedContent(postData);

        //    string responseFromServer = await httpClient.PostAsync(uri, content)
        //            .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
        //            .ContinueWith((readTask) => readTask.Result.Content.ReadAsStringAsync().Result);

        //    if (responseFromServer.StartsWith("true"))
        //    {
        //        // TODO: send an email blah blah

        //        return new HttpResponseMessage(HttpStatusCode.OK);
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Sorry mate, wrong captcha response. Are you a bot?");
        //    }

        //}
    }

    //public class ContactModel
    //{
    //    public string Response { get; set; }
    //}
}