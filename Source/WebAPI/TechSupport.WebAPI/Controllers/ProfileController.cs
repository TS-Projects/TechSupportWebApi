using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using TechSupport.Data.Models;
using TechSupport.Services.Data.Contracts;
using TechSupport.Services.Logic.Contracts;
using TechSupport.WebAPI.Controllers.Base;
using TechSupport.WebAPI.DataModels.Users;
using TechSupport.WebAPI.Infrastructure.Extensions;

namespace TechSupport.WebAPI.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IMappingService mappingService;

        public ProfileController(
            IUsersService usersService,
            IMappingService mappingService)
            : base(usersService)
        {
            this.mappingService = mappingService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string username)
        {
            var model = await this.UsersService
                .ByUsername(username)
                .ProjectTo<ProfileResponseModel>()
                .FirstOrDefaultAsync();

            return this.Data(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Post(ProfileRequestModel profile)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }
            var newFirstName = profile.FirstName;
            var newLastName = profile.LastName;
            var newCity = profile.City;
            var newPhone = profile.Phone;
            var newAbout = profile.About;

            var existingProfile = await this.UsersService
                .ByUsername(User.Identity.Name)
                .FirstOrDefaultAsync();

            await this.UsersService.UpdateUser(
                this.mappingService.Map(profile, existingProfile),
                newFirstName,
                newLastName,
                newCity,
                newPhone,
                newAbout
                );

            return this.Ok(this.mappingService.Map<ProfileResponseModel>(existingProfile));
            //return this.Ok(this.Get(User.Identity.Name));
        }

        //private void UpdateUser(User model, ProfileDataModel viewModel)
        //{
        //    model.FirstName = viewModel.FirstName;
        //    model.LastName = viewModel.LastName;
        //    model.City = viewModel.City;
        //    model.Phone = viewModel.Phone;
        //    model.About = viewModel.About;
        //}
    }
}