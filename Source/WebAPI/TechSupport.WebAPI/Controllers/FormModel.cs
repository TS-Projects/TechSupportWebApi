using Newtonsoft.Json;

namespace TechSupport.WebAPI.Controllers
{
    public class FormModel
    {
        //public string Name { get; set; }
        //public string Email { get; set; }

        [JsonProperty("g-recaptcha-response")]
        public string GRecaptchaResponse { get; set; }
    }
}