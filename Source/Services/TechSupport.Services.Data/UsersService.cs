using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TechSupport.Data.Common.Repositories;
using TechSupport.Data.Models;
using TechSupport.Services.Data.Contracts;

namespace TechSupport.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;
        public UsersService(IRepository<User> users)
        {
            this.users = users;
        }

        public IQueryable<User> ByUsername(string username)
        {
            return this.users
                .All()
                .Where(u => u.UserName == username);
        }

        public async Task UpdateUser(
             User user,
             string FirstName,
             string LastName,
             string Phone,
             string City,
             string About)
        {

            user.FirstName = FirstName;
            user.LastName = LastName;
            user.City = City;
            user.Phone = Phone;
            user.About = About;

            this.users.Update(user);
            await this.users.SaveChangesAsync();
        }

        public IQueryable<User> QueriedAllUsers()
        {
            var query = this.users
                .All()
                .Where(p => !p.IsHidden);
            return query;
        }

        public async Task<User> Account(string emailOrUser, string password)
        {
            //var remoteUser = await this.remoteData.Login(username, password);
            //if (remoteUser == null)
            //{
            //    return null;
            //}

            var localUser = await this.GetLocalAccount(emailOrUser);

            //if (localUser == null)
            //{
            //    localUser = new User
            //    {
            //        UserName = remoteUser.UserName
            //        //AvatarUrl = remoteUser.AvatarUrl,
            //        //IsAdmin = remoteUser.IsAdmin
            //    };

            //    this.users.Add(localUser);
            //    this.users.SaveChanges();
            //}
            //else if (localUser.AvatarUrl != remoteUser.AvatarUrl || localUser.IsAdmin != remoteUser.IsAdmin)
            //{
            //    localUser.IsAdmin = remoteUser.IsAdmin;
            //    localUser.AvatarUrl = remoteUser.AvatarUrl;
            //    this.users.SaveChanges();
            //}

            return localUser;
        }

        private async Task<User> GetLocalAccount(string emailOrUserName)
        {
            return await this.users
                .All()
                .FirstOrDefaultAsync(u => u.Email == emailOrUserName || u.UserName == emailOrUserName);
        }
    }
}
