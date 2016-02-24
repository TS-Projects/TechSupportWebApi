namespace TechSupport.WebAPI.DataModels
{
    using Newtonsoft.Json;

    public class CaptchaResponse
    {
        //public string Name { get; set; }
        //public string Email { get; set; }

        [JsonProperty("g-recaptcha-response")]
        public string GRecaptchaResponse { get; set; }
    }
}