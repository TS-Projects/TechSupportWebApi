using System;
using System.Linq;
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
    }
}
