using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.Data.Common.Repositories;
using TechSupport.Data.Models;

namespace TechSupport.Services.Data
{
    public class IdentityService
    {
        private readonly IRepository<User> users;

        public IdentityService(IRepository<User> users)
        {
            this.users = users;
        }

        public async Task<User> Account(string acc)
        {
            var localUser = await this.GetLocalAccount(acc);

            return localUser;
        }

        private async Task<User> GetLocalAccount(string acc)
        {
            return await this.users
                .All()
                .FirstOrDefaultAsync(u => u.SubUserName == acc);
        }
    }
}
