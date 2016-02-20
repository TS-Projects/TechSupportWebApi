using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.Data.Common.Repositories;
using TechSupport.Data.Models;

namespace TechSupport.Data
{
    public interface ICustomerCardsRepository : IRepository<CustomerCard>
    {
        CustomerCard GetWithContest(string password, string userId, bool isAllowed);

        bool Any(string password, string userId, bool isAllowed);
    }
}
