using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using TechSupport.Data;
using TechSupport.Data.Models;
using TechSupport.WebAPI.Api.Users.DataModels;
using TechSupport.WebAPI.Controllers;

namespace TechSupport.WebAPI.Api.Users.Controllers
{
    public class LoginController : BaseApiController
    {
        public LoginController(ITechSupportData data)
            : base(data)
        {
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var model = this.Data.Users.All()
                .Where(u => u.UserName == User.Identity.Name)
                .AsQueryable()
                .Project()
                .To<IdentityResponseModel>()
                .FirstOrDefault();

            return this.Ok(model);
        }
    }
}