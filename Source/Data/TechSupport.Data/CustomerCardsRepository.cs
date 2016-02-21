//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TechSupport.Data.Common.Repositories;
//using TechSupport.Data.Models;

//namespace TechSupport.Data
//{
//    class CustomerCardsRepository : EfGenericRepository<CustomerCard>
//    {
//        public CustomerCardsRepository(DbContext context)
//            : base(context)
//        {

//        }

//        public CustomerCard GetWithContest(string password, string userId)
//        {
//            return
//                this.All()
//                    .Include(x => x.User)
//                    .FirstOrDefault(x => x.CustomerCardPassword == password && x.UserId == userId);
//        }

//        public bool Any(string password, string userId, bool isAllowed)
//        {
//            return
//                this.All()
//                    .Any(x => x.CustomerCardPassword == password && x.UserId == userId);
//        }
//    }
//}
