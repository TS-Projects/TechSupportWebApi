using System.Data.Entity;
using System.Linq;
using TechSupport.Data.Models;
using TechSupport.Data.Repositories.Base;
using TechSupport.Data.Repositories.Contracts;

namespace TechSupport.Data.Repositories
{
    internal class CustomersRepository : GenericRepository<Customer>, ICustomersRepository
    {
        public CustomersRepository(ITechSupportDbContext context)
            : base(context)
        {
        }

        public Customer GetWithContest(int contestId, string userId, bool isOfficial)
        {
            return
                this.All()
                    .Include(x => x.CustomerCards)
                    .FirstOrDefault(x => x.Id == contestId && x.UserId == userId && x.IsOfficial == isOfficial);
        }

        public bool Any(int contestId, string userId, bool isOfficial)
        {
            return
                this.All()
                    .Any(x => x.Id == contestId && x.UserId == userId && x.IsOfficial == isOfficial);
        }
    }
}