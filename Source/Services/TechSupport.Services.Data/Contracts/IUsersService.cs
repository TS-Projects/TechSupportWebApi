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

        Task<User> FindUserById(string key);

        Task DeleteUser(User user);

        Task UpdateUser(
            User user,
            string FirstName,
            string LastName,
            string City,
            string Phone,
            string About);

    }
}