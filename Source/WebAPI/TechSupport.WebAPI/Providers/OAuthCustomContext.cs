using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;

namespace TechSupport.WebAPI.Providers
{
    public class OAuthCustomContext : OAuthGrantResourceOwnerCredentialsContext
    {
        public OAuthCustomContext(IOwinContext context, OAuthAuthorizationServerOptions options, string clientId, string userName, string password, IList<string> scope, string email)
            : base(context, options, clientId, userName, password, scope)
        {
            this.Email = email;
        }

        /// <summary>
        /// Resource owner email.
        /// 
        /// </summary>
        public string Email { get; private set; }

    }
}