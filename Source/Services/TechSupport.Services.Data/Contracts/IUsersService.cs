using System.Threading.Tasks;

namespace TechSupport.Services.Data.Contracts
{
    using System.Linq;
    using TechSupport.Data.Models;
    using TechSupport.Services.Common;

    public interface IUsersService : IService
    {
        IQueryable<User> ByUsername(string username);

        IQueryable<User> QueriedAllUsers();

        Task<User> Account(string emailOrUserName, string password);

        Task<bool> UserIsAdmin(string username);
    }
}
