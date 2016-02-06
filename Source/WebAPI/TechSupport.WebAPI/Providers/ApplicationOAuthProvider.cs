﻿using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TechSupport.Data.Models;
using TechSupport.Services.Data.Contracts;
using TechSupport.Services.Logic;
using TechSupport.WebAPI.Config;

namespace TechSupport.WebAPI.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private readonly IUsersService usersService;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public ApplicationOAuthProvider(string publicClientId, IUsersService usersService)
             : this(publicClientId)
        {
            this.usersService = usersService;
        }

        protected IUsersService UsersService
        {
            get { return this.usersService ?? ObjectFactory.Get<IUsersService>(); }
        }

        public static AuthenticationProperties CreateProperties()
        {
            return new AuthenticationProperties();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = await this.GetUserFromContext(context);

            if (user != null)
            {
                var oauthIdentity = ClaimsIdentityFactory.Create(user, OAuthDefaults.AuthenticationType);
                var cookiesIdentity = ClaimsIdentityFactory.Create(user, CookieAuthenticationDefaults.AuthenticationType);
 
                AuthenticationProperties properties = CreateProperties();
                AuthenticationTicket ticket = new AuthenticationTicket(oauthIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);
            }


        }

        private async Task<User> GetUserFromContext(OAuthGrantResourceOwnerCredentialsContext context)
        {
            User user = null;

            if (this.IsValidContext(context))
            {
                user = await this.LoginUser(context);
            }

            return user;
        }

        private async Task<User> LoginUser(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = await this.UsersService.Account(context.UserName, context.Password);

            // Check if remote login credentials are correct
            if (user == null)
            {
                context.SetError("invalid_grant", "Information is not valid");
                return null;
            }

            return user;
        }

        private bool IsValidContext(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var isValid = true;

            if (context == null)
            {
                isValid = false;
            }
            else if (string.IsNullOrEmpty(context.UserName) || string.IsNullOrWhiteSpace(context.UserName))
            {
                context.SetError("invalid_grant", "Information is not valid");
                isValid = false;
            }
            else if (string.IsNullOrEmpty(context.Password) || string.IsNullOrWhiteSpace(context.Password))
            {
                context.SetError("invalid_grant", "Information is not valid");
                isValid = false;
            }

            return isValid;
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}