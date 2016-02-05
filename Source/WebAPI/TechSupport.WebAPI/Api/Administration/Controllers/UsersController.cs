﻿using System;
using System.Data.Entity;
using AutoMapper;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;
using System.Web.OData.Routing;
using AutoMapper.QueryableExtensions;
using Elmah;
using TechSupport.Data;
using TechSupport.Services.Data.Contracts;
using TechSupport.WebAPI.Controllers;
using DataModel = TechSupport.WebAPI.DataModels.Administration.UserProfileDataModel;
using Model = TechSupport.Data.Models.User;

namespace TechSupport.WebAPI.Api.Administration.Controllers
{
    public class UsersController : ODataController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        [ODataRoute]
        public IQueryable<DataModel> Get()
        {
            var users = this.usersService
                .QueriedAllUsers()
                .AsQueryable()
                .ProjectTo<DataModel>();

            return users;
        }

        // [ODataRoute()]
        // [AllowAnonymous]
        // [HttpPut]
        // PUT odata/Users(5)
        
        public IHttpActionResult Put([FromODataUri] string key, [FromBody]DataModel user)
        {
            var customEx = new Exception("Hello I am testing Elmah", new NotSupportedException());
            ErrorSignal.FromCurrentContext().Raise(customEx);
            throw new Exception();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (key != user.Id)
            //{
            //    return BadRequest();
            //}

            ////if (this.data.GetUserProfile(user.Id) == null)
            ////{
            ////    return NotFound();
            ////}

            //var dbModel = this.Data.Users.GetById(user.Id);
            //Mapper.Map<DataModel, Model>(user, dbModel);
            //// _dataService.Save(customer);
            //this.Data.Users.Update(dbModel);

            //try
            ////await db.SaveChangesAsync();
            //{
            //    this.Data.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UserExists(key))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return this.Updated(user);
        }

        //public IHttpActionResult Post(UserAdministrationDataModel vm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var dm = Mapper.Map<Model>(vm);
        //    this.Data.Create(dm);

        //    try

        //    {
        //        this.Data.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (UserExists(vm.Id))
        //        {
        //            return Conflict();
        //        }
        //        throw;
        //    }
        //    vm.Id = dm.Id;
        //    return Created(vm);
        //}
        //private bool UserExists(string userId)
        //{
        //    return this.Data.Users.All().Select(u => u.Id == userId && !u.IsHidden).Any();
        //}
    }
}