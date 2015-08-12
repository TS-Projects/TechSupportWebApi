using TechSupport.Data.Common;
using TechSupport.Data.Models;

namespace TechSupport.Data.Repositories.Contracts
{
    public interface ICustomersRepository : IRepository<Customer>
    {
        Customer GetWithContest(int contestId, string userId, bool isOfficial);

        bool Any(int contestId, string userId, bool isOfficial);
    }
}