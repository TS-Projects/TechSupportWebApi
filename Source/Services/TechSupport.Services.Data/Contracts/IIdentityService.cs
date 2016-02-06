using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.Data.Models;
using TechSupport.Services.Common;

namespace TechSupport.Services.Data.Contracts
{
    public interface IIdentityService : IService
    {
        Task<User> Account(string acc);
    }
}
