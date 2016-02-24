using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;

namespace TechSupport.WebAPI.Infrastructure
{
    public class ReCaptcha
    {
        public bool Success { get; set; }
        public List<string> ErrorCodes { get; set; }

        public static bool Validate(string encodedResponse)
        {
            if (string.IsNullOrEmpty(encodedResponse)) return false;

            var client = new System.Net.WebClient();
            //var secret = ConfigurationManager.AppSettings["Google.ReCaptcha.Secret"];
            string secret = "6LeyHRkTAAAAAD-nRFFC3iK8pIggHj5-C7ZgFeKo";

            if (string.IsNullOrEmpty(secret)) return false;

            var googleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, encodedResponse));

            var serializer = new JavaScriptSerializer();

            var reCaptcha = serializer.Deserialize<ReCaptcha>(googleReply);

            return reCaptcha.Success;
        }

        //public static string Validate(string EncodedResponse)
        //{
        //    var client = new WebClient();

        //    string PrivateKey = "6LeyHRkTAAAAAD-nRFFC3iK8pIggHj5-C7ZgFeKo";

        //    var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

        //    var captchaResponse = JsonConvert.DeserializeObject<ReCaptcha>(GoogleReply);

        //    return captchaResponse.Success;
        //}

        //[JsonProperty("success")]
        //public string Success
        //{
        //    get { return m_Success; }
        //    set { m_Success = value; }
        //}

        //private string m_Success;
        //[JsonProperty("error-codes")]
        //public List<string> ErrorCodes
        //{
        //    get { return m_ErrorCodes; }
        //    set { m_ErrorCodes = value; }
        //}


        //private List<string> m_ErrorCodes;
    }
}
