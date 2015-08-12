using TechSupport.Data.Common;
using TechSupport.Data.Models;
using TechSupport.Data.Repositories.Contracts;

namespace TechSupport.Data
{
    public interface ITechSupportData
    {
        IDeletableEntityRepository<User> Users { get; }

        IDeletableEntityRepository<CustomerCard> CustomerCards { get; }

        IDeletableEntityRepository<CustomerCardCategory> CustomerCardCategories { get; }

        IDeletableEntityRepository<CustomerCardQuestion> CustomerCardQuestions { get; }

        IDeletableEntityRepository<CustomerCardQuestionAnswer> CustomerCardQuestionAnswers { get; }

        IDeletableEntityRepository<CustomerAnswer> CustomerAnswers { get; }

        ICustomersRepository Customers { get; }

        ITechSupportDbContext Context { get; }

        void SaveChanges();
    }
}