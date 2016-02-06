using System;
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

        public IQueryable<User> QueriedAllUsers()
        {
            var query = this.users
                .All()
                .Where(p => !p.IsHidden);
            return query;
        }

        public async Task<User> Account(string email, string password)
        {
            //var remoteUser = await this.remoteData.Login(username, password);
            //if (remoteUser == null)
            //{
            //    return null;
            //}

            var localUser = await this.GetLocalAccount(email);
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

        private async Task<User> GetLocalAccount(string email)
        {
            return await this.users
                .All()
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
