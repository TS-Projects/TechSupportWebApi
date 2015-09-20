﻿namespace TechSupport.Services.Data.Contracts
{
    using System.Linq;
    using TechSupport.Data.Models;
    using TechSupport.Services.Common;

    public interface IUsersService : IService
    {
        IQueryable<User> ByUsername(string username);

        IQueryable<User> QueriedAllUsers();
    }
}