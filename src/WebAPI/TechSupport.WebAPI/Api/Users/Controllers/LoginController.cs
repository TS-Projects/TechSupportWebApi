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
using TechSupport.Services.Data.Contracts;
using TechSupport.WebAPI.Api.Users.DataModels;
using TechSupport.WebAPI.Controllers;

namespace TechSupport.WebAPI.Api.Users.Controllers
{
    public class LoginController : ApiController
    {
        private readonly IUsersService usersService;

        public LoginController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var model = this.usersService
                .QueriedAllUsers()
                .Where(u => u.UserName == this.User.Identity.Name)
                .AsQueryable()
                .Project()
                .To<IdentityResponseModel>()
                .FirstOrDefault();

            return this.Ok(model);
        }
    }
}