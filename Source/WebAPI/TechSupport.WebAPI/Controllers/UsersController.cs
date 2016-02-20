using System;
using System.Data.Entity;
using AutoMapper;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;
using System.Web.OData.Routing;
using AutoMapper.QueryableExtensions;
using Elmah;
using TechSupport.Data;
using TechSupport.Services.Data.Contracts;
using TechSupport.WebAPI.Controllers;
using ResponseDataModel = TechSupport.WebAPI.DataModels.Administration.ResponseDataModel;
using Model = TechSupport.Data.Models.User;

namespace TechSupport.WebAPI.Controllers
{
    public class UsersController : ODataController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [EnableQuery]
        [ODataRoute]
        public IQueryable<ResponseDataModel> Get()
        {
            var users = this.usersService
                .QueriedAllUsers()
                .AsQueryable()
                .ProjectTo<ResponseDataModel>();

            return users;
        }

        // [ODataRoute()]
        // [AllowAnonymous]
        // [HttpPut]
        // PUT odata/Users(5)

        public IHttpActionResult Put([FromODataUri] string key, [FromBody]ResponseDataModel user)
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

        //public IHttpActionResult Delete([FromODataUri] string key)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var user = this.usersService
        //        .QueriedAllUsers()
        //        .AsQueryable()
        //        .FirstOrDefault(u => u.Id == key);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }


        //    return Content(HttpStatusCode.NoContent, "Deleted");
        //}

        [ODataRoute]
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            var product = await usersService.FindUserById(key);
            if (product == null)
            {
                return NotFound();
            }
            await usersService.DeleteUser(product);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}