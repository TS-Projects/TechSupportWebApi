using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TechSupport.Data.Models;

namespace TechSupport.Data
{
    public interface ITechSupportDbContext
    {
        IDbSet<CustomerCard> CustomerCards { get; }

        IDbSet<Customer> Customers { get; }

        IDbSet<CustomerCardCategory> CustomerCardCategories { get; }

        IDbSet<CustomerCardQuestion> CustomerCardQuestions { get; }

        IDbSet<CustomerCardQuestionAnswer> CustomerCardQuestionAnswers { get; }

        IDbSet<CustomerAnswer> CustomerAnswers { get; }

        DbContext DbContext { get; }

        void Dispose();

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}